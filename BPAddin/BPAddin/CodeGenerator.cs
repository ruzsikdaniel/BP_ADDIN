using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;

namespace BPAddin
{
    public partial class CodeGenerator : Form
    {
        private EA.Repository repo;

        private EAClass chosenClass = new EAClass();
        private List<EAClass> cls = new List<EAClass>();
        private List<EA.Package> packages = new List<EA.Package>();


        public CodeGenerator(EA.Repository repository)
        {
            InitializeComponent();
            repo = repository;
        }

        public void setLblText(string text){
            lblText.Text = text;
        }

        public void initPackages(List<EA.Package> pkgs) { 
            cbxClasses.Items.Clear();
            packages.Clear();
            packages.AddRange(pkgs);

            foreach (EA.Package pkg in pkgs){
                cbxClasses.Items.Add(pkg.Name);
            }
        }

        public void initClasses(List<EAClass> classes){
            cbxClasses.Items.Clear();
            
            cls.Clear();
            cls.AddRange(classes);

            foreach (EAClass c in classes) {
                cbxClasses.Items.Add(c.clsName);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int index = cbxClasses.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please choose a class.");
                return;
            }

            EA.Package selectedPackage = packages[index];

            string generatedDir_test = "C:\\_School\\BP\\funkcny_prototyp\\BP_ADDIN\\class_generation_output";
            string uiLibDir_test = "C:\\_School\\BP\\funkcny_prototyp\\BP_ADDIN\\BPAddin\\BPAddin\\UI_Library";
            string outputDir_test = "C:\\_School\\BP\\funkcny_prototyp\\project";

            /*
            string generatedDir = ask_for_directory("Choose a folder for generated .cs files");
            if (selectedPackage == null)
                return;
            
            // test
            string same = "different";
            if (generatedDir == generatedDir_test)
                same = "same";
            MessageBox.Show("generatedDir_test - " + generatedDir_test +
                "\n\ngeneratedDir - " + generatedDir +
                "\n" + same);
            */

            setPackageFilepaths(selectedPackage, generatedDir_test);

            try {
                EA.Project proj = repo.GetProjectInterface();
                string pkgGuid = proj.GUIDtoXML(selectedPackage.PackageGUID);
                proj.GeneratePackage(pkgGuid, "");
                MessageBox.Show("EA generation complete.\n Files: " + generatedDir_test, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error during EA generation: \n\n" + ex.Message, "BPAddin - error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /*
            string uiLibDir = ask_for_directory("Choose folder of UI Library.");
            if(uiLibDir == null)
                return;

            string outputDir = ask_for_directory("Choose folder for the output WinForms projekt.");
            if (outputDir == null)
                return;
            */
            

            ProjectBuilder builder = new ProjectBuilder(generatedDir_test, uiLibDir_test, outputDir_test);
            builder.buildProject();

            Close();


            /*EA.Project project = repo.GetProjectInterface();
            string pkg_guid = project.GUIDtoXML(chosenClass.pkg.PackageGUID);

            project.GeneratePackage(pkg_guid, "");

            MessageBox.Show("Code generated for class: " + chosenClass.clsName);
            Close();
            */
        }

        private void setPackageFilepaths(EA.Package pkg, string dir) {
            foreach (EA.Element el in pkg.Elements) {
                if (el.Type == "Class") {
                    el.Gentype = "C#";

                    string filepath = Path.Combine(dir, el.Name + ".cs");

                    if (el.Files.Count == 0) {
                        EA.File f = (EA.File)el.Files.AddNew(filepath, "C#");
                        f.Update();
                    }
                    else {
                        EA.File f = (EA.File)el.Files.GetAt(0);
                        f.Name = "";
                        //MessageBox.Show("Element " + el.Name + " has Filename of " + f.Name);

                        f.Name = filepath;

                        //MessageBox.Show("New Filename - " + f.Name);
                        f.Update();
                    }

                    el.Files.Refresh();
                    el.Update();
                    /*
                    if (!System.IO.File.Exists(filepath))
                    {
                        System.IO.Directory.CreateDirectory(dir);
                        System.IO.File.WriteAllText(filepath, "");
                        MessageBox.Show("File not found at " +  + ", created empty .cs for " + filepath);
                    }
                    */
                }
            }

            foreach (EA.Package sub in pkg.Packages) {
                setPackageFilepaths(sub, dir);
            }
        }

        private string ask_for_directory(string desc) {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog()) { 
                dlg.Description = desc;
                dlg.ShowNewFolderButton = true;

                if (dlg.ShowDialog() == DialogResult.OK) {
                    return dlg.SelectedPath;
                }
                return null;
            }
        }
    }
}
