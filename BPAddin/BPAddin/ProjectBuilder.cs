using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{
    public class ProjectBuilder
    {
        private string generatedSrcDir;  // directory for EA-generated .cs files
        private string uiLibrarySrcDir;  // directory for UI_Library elements
        private string outputProjectDir; // directory for project created from generated files

        private List<string> screenNames = new List<string>();

        public ProjectBuilder(string generatedSrcDir, string uiLibrarySrcDir, string outputProjectDir)
        {
            this.generatedSrcDir = generatedSrcDir;
            this.uiLibrarySrcDir = uiLibrarySrcDir;
            this.outputProjectDir = outputProjectDir;
        }

        public void setScreenNames(List<string> names) { 
            this.screenNames = names;
        }

        public void buildProject()
        {
            try
            {
                runCommand("dotnet", "new winforms -o \"" + outputProjectDir + "\" --force");
                copyUILibrary();
                
                mergeForm1Designer();

                writeCustomProgramCs();

                cleanOldDirectives(outputProjectDir);


                // build the project ->.exe file
                runCommand("dotnet", "build \"" + outputProjectDir + "\"");

                // find the .exe file
                string exePath = findExe(outputProjectDir);
                if (exePath != null)
                {
                    if(MessageBox.Show("Compilation successful!\n\n.exe file:\n" + exePath + "\n\nDo you want to launch the .exe file?", "BPAddin – Success", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Process.Start(exePath);
                }
                else{
                    MessageBox.Show("Compilation has finished, .exe was not found.\nPlease refer to: " + outputProjectDir, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during compilation:\n\n" + ex.Message, "BPAddin – Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void copyUILibrary()
        {
            string assetsDir = Path.Combine(outputProjectDir, "ui_assets");

            if (Directory.Exists(assetsDir))
                Directory.Delete(assetsDir, true);

            Directory.CreateDirectory(assetsDir);

            copyCSFiles(this.uiLibrarySrcDir, assetsDir);   // Screen.cs, Button.cs...

            foreach (string file in Directory.GetFiles(this.generatedSrcDir, "*.Designer.cs", SearchOption.TopDirectoryOnly))
            {
                string fileName = Path.GetFileName(file);
                string dest = Path.Combine(outputProjectDir, fileName);
                File.Copy(file, dest, overwrite: true);
            }


            foreach (string file in Directory.GetFiles(this.generatedSrcDir, "*.cs", SearchOption.TopDirectoryOnly))
            {
                string fileName = Path.GetFileName(file);
                if (fileName == "Program.cs") continue;
                if (fileName.EndsWith(".Designer.cs")) continue;  // <-- pridaj toto

                string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                bool isScreen = screenNames.Contains(nameWithoutExt);

                string dest = isScreen ? Path.Combine(outputProjectDir, fileName) : Path.Combine(assetsDir, fileName);

                File.Copy(file, dest, overwrite: true);
            }
        }


        private void copyCSFiles(string sourceDir, string destDir, SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (!Directory.Exists(sourceDir))
            {
                MessageBox.Show("Folder does not exist: " + sourceDir, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (string file in Directory.GetFiles(sourceDir, "*.cs", searchOption))
            {
                string fileName = Path.GetFileName(file);
                if (fileName == "Program.cs") 
                    continue;

                string dest = Path.Combine(destDir, fileName);
                File.Copy(file, dest, overwrite: true);
            }
        }


        private void mergeForm1Designer()
        {
            if (screenNames.Count == 0) 
                return;

            // first screen in array is default
            string mainScreen = screenNames[0];

            string form1DesPath = Path.Combine(outputProjectDir, "Form1.Designer.cs");
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
            string mainDesPath = Path.Combine(outputProjectDir, mainScreen + ".Designer.cs");   // path - ./project/<ScreenName>.Designer.cs
            string mainDesContent = File.ReadAllText(mainDesPath);

            string oldInit = "        this.SuspendLayout();\r\n        this.ResumeLayout(false);\r\n";
            mainDesContent = mainDesContent.Replace(oldInit, initBody);

            // replace Form1 with main screen's name
            mainDesContent = mainDesContent.Replace("Form1", mainScreen);

            File.WriteAllText(mainDesPath, mainDesContent);

            File.Delete(Path.Combine(outputProjectDir, "Form1.cs"));
            File.Delete(Path.Combine(outputProjectDir, "Form1.Designer.cs"));
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
        private void writeCustomProgramCs()
        {
            if (screenNames.Count == 0) return;
            string mainScreen = screenNames[0];

            string content =
                "using System;\r\n" +
                "using System.Windows.Forms;\r\n\r\n" +
                "static class Program\r\n{\r\n" +
                "    [STAThread]\r\n" +
                "    static void Main()\r\n    {\r\n" +
                "        Application.EnableVisualStyles();\r\n" +
                "        Application.SetCompatibleTextRenderingDefault(false);\r\n" +
                "        Application.Run(new " + mainScreen + "());\r\n" +
                "    }\r\n}\r\n";

            File.WriteAllText(Path.Combine(outputProjectDir, "Program.cs"), content);
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
                    throw new Exception("Command failed: " + command + " " + args + "\n\nOutput:\n" + output + "\n\nError:\n" + error);
                }
            }
        }

        private void cleanOldDirectives(string outputDir) {
            foreach (string file in Directory.GetFiles(outputDir, "*.cs", SearchOption.AllDirectories)){ 
                string content = File.ReadAllText(file); 
                
                content = content.Replace("using BPAddin.UI_Library;\r\n", "");
                content = content.Replace("using BPAddin.UI_Library;\n", "");
                content = content.Replace("using BPAddin.UI_LIbrary;\r\n", "");
                content = content.Replace("using BPAddin.UI_LIbrary;\n", "");

                File.WriteAllText(file, content);
            }
        }
    }
}
