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

        public ProjectBuilder(string generatedSrcDir, string uiLibrarySrcDir, string outputProjectDir)
        {
            this.generatedSrcDir = generatedSrcDir;
            this.uiLibrarySrcDir = uiLibrarySrcDir;
            this.outputProjectDir = outputProjectDir;
        }

        public void buildProject()
        {
            try
            {
                // create new WinForms project
                runCommand("dotnet", "new winforms -o \"" + outputProjectDir + "\" --force");

                // copy EA-generated .cs fiels
                //copyCSFiles(generatedSrcDir, outputProjectDir);

                // copy 'all' UI_Library components
                // TODO: fetch only the used UI_Library compontents from UI_Library
                // - possibly to an internal directory to segragate from other project classes

                copyUILibrary();
                //copyCSFiles(uiLibrarySrcDir, outputProjectDir);

                cleanOldDirectives(outputProjectDir);

                //addUsingToGenerated(outputProjectDir);

                // build the project ->.exe file
                runCommand("dotnet", "build \"" + outputProjectDir + "\"");

                // find the .exe file
                string exePath = findExe(outputProjectDir);
                if (exePath != null)
                {
                    MessageBox.Show(
                        "Compilation successful!\n\n.exe file:\n" + exePath + "\n\nDo you want to launch the .exe file?", "BPAddin – Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information
                    );
                    // execute .exe file upon user's choice
                    if(MessageBox.Show("Execute .exe file?", "BPAddin", MessageBoxButtons.YesNo) == DialogResult.Yes){
                        Process.Start(exePath);     // run the .exe file
                    }
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
            copyCSFiles(this.generatedSrcDir, assetsDir);   // scrMain.cs, screenScreenA.cs...
        }

        private void copyUILibrary_old() {

            string assetsDirName = "ui_assets";
            string assetsDir = Path.Combine(outputProjectDir, assetsDirName);
            
            // empty the "./ui_assets folder"
            if (Directory.Exists(assetsDir)) {
                Directory.Delete(assetsDir, true);
            }

            // create empty "./ui_assets" folder
            Directory.CreateDirectory(assetsDir);

            
            // find name for source UI_Library folder
            string uiLibrarySrcDirName = Path.GetFileName(this.uiLibrarySrcDir);

            // create "./ui_assets/%UI_Library%" path and folder
            string uiLibraryDir = Path.Combine(outputProjectDir, uiLibrarySrcDirName);
            Directory.CreateDirectory(uiLibraryDir);    

            copyCSFiles(this.uiLibrarySrcDir, assetsDir);   // Screen.cs, Button.cs, ...
            copyCSFiles(this.generatedSrcDir, assetsDir);   // screenScreenA.cs, btnOK.cs, ...
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

        private void addUsingToGenerated(string outputDir)
        {
            string header = "using System;\r\nusing System.Drawing;\r\nusing System.Windows.Forms;\r\nusing BPAddin;\r\n\r\n";

            foreach (string file in Directory.GetFiles(outputDir, "*.cs", SearchOption.TopDirectoryOnly))
            {
                string filename = Path.GetFileName(file);
                if (filename == "Program.cs")
                    continue;

                string content = File.ReadAllText(file);

                if (!content.TrimStart().StartsWith("using"))
                {
                    File.WriteAllText(file, header + content);
                }
            }
        }
    }
}
