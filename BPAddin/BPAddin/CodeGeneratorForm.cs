using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EA;

namespace BPAddin
{
    public partial class CodeGeneratorForm : Form
    {
        private CodeGenerator cg;

        public CodeGeneratorForm(EA.Repository repository)
        {
            InitializeComponent();
            this.cg = new CodeGenerator(repository);
        }

        public void setLblText(string text){
            lblText.Text = text;
        }

        public void initPackages(List<EA.Package> pkgs) { 
            cbxPackages.Items.Clear();
            cg.packages.Clear();
            cg.packages.AddRange(pkgs);

            foreach (EA.Package pkg in pkgs){
                cbxPackages.Items.Add(pkg.Name);
            }
        }

        public void initClasses(List<EAClass> classes){
            cbxPackages.Items.Clear();
            
            cg.cls.Clear();
            cg.cls.AddRange(classes);

            foreach (EAClass c in classes) {
                cbxPackages.Items.Add(c.clsName);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int index = cbxPackages.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please choose a class.");
                return;
            }

            EA.Package selectedPackage = cg.packages[index];

            string generatedDir = Properties.Settings.Default.GeneratedDir;
            string uiLibDir = Properties.Settings.Default.UILibraryDir;
            string outputDir = Properties.Settings.Default.ProjectDir;


            // TODO: check if actually changes filepaths for class elements
            cg.setPackageFilepaths(selectedPackage, generatedDir);

            cg.initScreenClasses(selectedPackage);

            cg.initDesigners(generatedDir);

            try {
                EA.Project proj = cg.repo.GetProjectInterface();
                string pkgGuid = proj.GUIDtoXML(selectedPackage.PackageGUID);

                if (MessageBox.Show("Ready to generate?", "BPAddin", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;

                proj.GeneratePackage(pkgGuid, "");
                MessageBox.Show("EA generation complete.\n Files: " + generatedDir, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error during EA generation: \n\n" + ex.Message, "BPAddin - error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> screenNames = cg.screens.Select(el => el.Name).ToList();

            ProjectBuilder builder = new ProjectBuilder(generatedDir, uiLibDir, outputDir);
            builder.setScreenNames(screenNames);
            builder.buildProject();

            Close();
            
        }
    }
}
