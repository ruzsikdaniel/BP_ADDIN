using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;

namespace BPAddin
{
    public class ProjectBuilder
    {
        private string generatedSrcDir;  // directory for EA-generated .cs files
        private string uiLibrarySrcDir;  // directory for UILibrary elements
        private string projectDir; // directory for project created from generated files

        private List<string> screenNames = new List<string>();

        private Process prototypeProcess;
        private string programNamespace = "";

        public void configure(string generatedSrcDir, string uiLibrarySrcDir, string projectDir)
        {
            this.generatedSrcDir = generatedSrcDir;
            this.uiLibrarySrcDir = uiLibrarySrcDir;
            this.projectDir = projectDir;
        }

        public void setScreenNames(List<string> names) { 
            this.screenNames = names;
        }

        public void setProgramNamespace(string ns)
        {
            this.programNamespace = ns;
        }

        public void buildProject()
        {
            string tempDir = projectDir + "_temp";
            try
            {
                if (prototypeProcess != null && !prototypeProcess.HasExited)
                {
                    prototypeProcess.Kill();
                    prototypeProcess.WaitForExit();
                }

                if (Directory.Exists(projectDir)) {
                    Directory.Delete(projectDir, true );
                }

                runCommand("dotnet", "new winforms -o \"" + projectDir + "\" --force");

                Directory.Move(projectDir, tempDir);

                collectUIAssets(tempDir);
                mergeForm1Designer(tempDir);
                updateProgramFile(tempDir);
                cleanOldDirectives(tempDir);
                
                if (Directory.Exists(projectDir))
                    Directory.Delete(projectDir, true);

                Directory.Move(tempDir, projectDir);


                // build the project ->.exe file
                runCommand("dotnet", "build \"" + projectDir + "\"");                

                // find the .exe file
                string exePath = findExe(projectDir);
                if (exePath != null)
                {
                    if(MessageBox.Show("Compilation successful!\n\n.exe file:\n" + exePath + "\n\nDo you want to launch the .exe file?", "BPAddin – Success", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        launchPrototype(exePath);
                }
                else{
                    MessageBox.Show("Compilation has finished, .exe was not found.\nPlease refer to: " + projectDir, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }

                MessageBox.Show("Error during compilation:\n\n" + ex.Message, "BPAddin – Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void launchPrototype(string exePath)
        {
            if (prototypeProcess != null && !prototypeProcess.HasExited)
            {
                if (MessageBox.Show("Prototype is already running. Close it and launch new?",
                    "BPAddin", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    prototypeProcess.Kill();
                    prototypeProcess.WaitForExit();
                }
                else return;
            }
            prototypeProcess = new Process();
            prototypeProcess.StartInfo.FileName = exePath;
            prototypeProcess.EnableRaisingEvents = true;
            prototypeProcess.Exited += (s, e) => prototypeProcess = null;
            prototypeProcess.Start();
        }


        private void collectUIAssets(string projectDir)
        {
            string assetsDir = Path.Combine(projectDir, "ui_assets");

            if (Directory.Exists(assetsDir))
                Directory.Delete(assetsDir, true);

            Directory.CreateDirectory(assetsDir);

            copyUILibrary(assetsDir);   // UIScreen.cs, UIButton.cs...
            moveGeneratedCS(assetsDir, projectDir); // buttonButtonOK.cs, screenScreenA.cs, ...
        }

        private void moveGeneratedCS(string assetsDir, string projectDir) {
            if (!Directory.Exists(generatedSrcDir))
            {
                MessageBox.Show("Folder for generated files does not exist: " + generatedSrcDir, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            foreach (string file in Directory.GetFiles(generatedSrcDir, "*.Designer.cs", SearchOption.TopDirectoryOnly))
            {
                string fileName = Path.GetFileName(file);
                string dest = Path.Combine(projectDir, fileName);

                File.Copy(file, dest, overwrite: true);
            }


            foreach (string file in Directory.GetFiles(generatedSrcDir, "*.cs", SearchOption.TopDirectoryOnly))
            {
                string fileName = Path.GetFileName(file);
                if (fileName == "Program.cs")
                    continue;

                if (fileName.EndsWith(".Designer.cs"))
                    continue;

                string fileNameRaw = Path.GetFileNameWithoutExtension(fileName);
                bool isScreen = screenNames.Contains(fileNameRaw);

                string dest;
                if (isScreen)
                    dest = Path.Combine(projectDir, fileName);
                else
                    dest = Path.Combine(assetsDir, fileName);

                File.Copy(file, dest, overwrite: true);
            }
        }


        private void copyUILibrary(string assetsDir)
        {

            foreach (string file in Directory.GetFiles(uiLibrarySrcDir, "*.cs", SearchOption.TopDirectoryOnly))
            {
                string fileName = Path.GetFileName(file);

                // TODO: use dynamic map of UI Library element file names instead
                if (!fileName.StartsWith("UI"))
                    continue;

                string dest = Path.Combine(assetsDir, fileName);


                // copy each "UI...cs" files from the UI Library directory
                File.Copy(file, dest, overwrite: true);
            }
        }


        private void mergeForm1Designer(string dest)
        {
            if (screenNames.Count == 0) 
                return;

            // first screen in array is default
            string mainScreen = screenNames[0];

            string form1DesPath = Path.Combine(dest, "Form1.Designer.cs");
            if (!File.Exists(form1DesPath)) 
                return;

            string form1Content = File.ReadAllText(form1DesPath);

            // get content of InitializeComponent() from Form1.Designer.cs
            string start = "private void InitializeComponent()";
            int strIndex = form1Content.IndexOf(start);
            if (strIndex < 0) 
                return;

            // find method body
            int braceStart = form1Content.IndexOf('{', strIndex);
            int braceEnd = findMatchingBrace(form1Content, braceStart);
            string initBody = form1Content.Substring(braceStart + 1, braceEnd - braceStart - 1);

            // write Designer.cs into main screen
            string mainDesPath = Path.Combine(dest, mainScreen + ".Designer.cs");   // path - ./project/<ScreenName>.Designer.cs
            string mainDesContent = File.ReadAllText(mainDesPath);

            string oldInit = "        this.SuspendLayout();\r\n        this.ResumeLayout(false);\r\n";
            mainDesContent = mainDesContent.Replace(oldInit, initBody);

            // replace Form1 with main screen's name
            mainDesContent = mainDesContent.Replace("Form1", mainScreen);

            File.WriteAllText(mainDesPath, mainDesContent);

            File.Delete(Path.Combine(dest, "Form1.cs"));
            File.Delete(Path.Combine(dest, "Form1.Designer.cs"));
        }

        private int findMatchingBrace(string text, int openBrace)
        {
            int depth = 0;
            for (int i = openBrace; i < text.Length; i++)
            {
                if (text[i] == '{') 
                    depth++;
                else if (text[i] == '}') 
                    depth--;
                if (depth == 0) 
                    return i;
            }
            return -1;
        }

        private void updateProgramFile(string dest)
        {
            if (screenNames.Count == 0) return;
            string mainScreen = screenNames[0];

            string content =
                "using System;\r\n" +
                "using System.Windows.Forms;\r\n" +
                "using " + programNamespace + ";\r\n\r\n" +
                "static class Program\r\n{\r\n" +
                "    [STAThread]\r\n" +
                "    static void Main()\r\n    {\r\n" +
                "        Application.EnableVisualStyles();\r\n" +
                "        Application.SetCompatibleTextRenderingDefault(false);\r\n" +
                "        Application.Run(new " + mainScreen + "());\r\n" +
                "    }\r\n}\r\n";

            File.WriteAllText(Path.Combine(dest, "Program.cs"), content);
        }
        

        private string findExe(string projectDir)
        {
            // find .exe either in bin/Debug or bin/Release
            string[] searchDirs = new string[]
            {
                Path.Combine(projectDir, "bin", "Debug"),
                Path.Combine(projectDir, "bin", "Release"),
            };

            foreach (string dir in searchDirs)
            {
                if(!Directory.Exists(dir))
                    continue;

                foreach (string exe in Directory.GetFiles(dir, "*.exe", SearchOption.AllDirectories))
                {
                    return exe;
                }
            }
            return null;
        }

        private void runCommand(string command, string args)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    string displayMessage = filterBuildErrors(output + "\n" + error);
                    throw new Exception(displayMessage);
                }
            }
        }

        private string filterBuildErrors(string rawOutput) {
            var errors = new List<string>();
            foreach (string line in rawOutput.Split('\n'))
            {
                string trimmed = line.Trim();
                if (trimmed.Contains(": error "))
                    errors.Add(trimmed);
            }

            if (errors.Count == 0)
                return "Build failed. No specific errors found.\n\nRaw output:\n" + rawOutput;

            return "Build failed with " + errors.Count + " error(s):\n\n" + string.Join("\n\n", errors);

        }

        private void cleanOldDirectives(string outputDir) {
            string classInstanceName = "Instance";

            foreach (string file in Directory.GetFiles(outputDir, "*.cs", SearchOption.AllDirectories)){ 
                string content = File.ReadAllText(file);
                
                content = content.Replace("using BPAddin.UILibrary;\r\n", "");
                content = content.Replace("using BPAddin.UILibrary;\n", "");
                content = content.Replace("using BPAddin.UILIbrary;\r\n", "");
                content = content.Replace("using BPAddin.UILIbrary;\n", "");
                
                content = System.Text.RegularExpressions.Regex.Replace(
                    content,
                    @"(?i)public void (on\w+Click)\(\)",            // case insensitive for the event name - onbuttonNewClick vs. onButtonNewClick
                    "private void $1(object sender, EventArgs e)"
                );
                

                content = System.Text.RegularExpressions.Regex.Replace(
                    content,
                    @"public (\w+)\(\)\s*\{\s*\r?\n\s*InitializeComponent\(\);",
                    "public static $1 "+ classInstanceName +";\r\n\r\n        public $1()\r\n        " +
                    "{\r\n            " +
                    "InitializeComponent();\r\n            " +
                    "$1." + classInstanceName + " = this;"
                );

                File.WriteAllText(file, content);
            }


            string assetsDir = Path.Combine(outputDir, "ui_assets");
            foreach(string file in Directory.GetFiles(assetsDir, "*.cs", SearchOption.AllDirectories))
            {
                string content = File.ReadAllText(file);
                if (!content.Contains("namespace BPAddin") && !content.Contains("using BPAddin;")) {
                    content = "using BPAddin;\r\n" + content;
                    File.WriteAllText(file, content);
                }
            }
        }
    }
}
