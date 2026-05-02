using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EA;
using BPAddin.Util;
using System.IO;


using static BPAddin.util.EABase;
using static BPAddin.util.EAMacros;
using BPAddin.model;

namespace BPAddin
{
    public partial class CodeGeneratorForm : Form
    {
        private CodeGenerator cg;
        private EA.Repository repo;
        private ProjectBuilder builder;
        
        public CodeGeneratorForm(EA.Repository repository, ProjectBuilder builder)
        {
            InitializeComponent();
            this.repo = repository;
            this.builder = builder;
            this.cg = new CodeGenerator(repository);
        }

        public void setLblText(string text){
            lblText.Text = text;
        }

        public void setLblMainScreen(string text)
        {
            lblMainScreen.Text = text;
        }

        public void initComboboxes(List<EA.Package> pkgs) { 
            cbxPackages.Items.Clear();
            cg.packages.Clear();

            cg.packages.AddRange(pkgs);

            foreach (EA.Package pkg in pkgs){
                cbxPackages.Items.Add(pkg.Name);
            }

            cbxPackages.Text = Properties.Settings.Default.AppCDPkg;
            tbxGenerated.Text = Properties.Settings.Default.GeneratedDir;
            tbxProject.Text = Properties.Settings.Default.ProjectDir;

            // fill cbxMainScreen with screen names
            string appPkgName = Properties.Settings.Default.AppCDPkg;
            EA.Package appPkg = findPackageByName(repo, appPkgName);

            cg.uiScreen = null;
            cg.initScreenClasses(appPkg);

            cbxMainScreen.Items.Clear();
            foreach (EA.Element screen in cg.screens)
                cbxMainScreen.Items.Add(screen.Name);

            cbxMainScreen.SelectedIndex = 0;
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

            string uiLibDir = AddinPaths.UILibraryPath; 
            string generatedDir = Properties.Settings.Default.GeneratedDir;
            string outputDir = Properties.Settings.Default.ProjectDir;

            cg.setPackageFilepaths(selectedPackage, generatedDir);
            cg.initScreenClasses(selectedPackage);

            List<string> errors = cg.validateAppModelNaming(cg.screens, selectedPackage);
            //MessageBox.Show("errors in naming: " + errors.Count);
            if (errors.Count > 0)
            {
                MessageBox.Show(
                    string.Join("\n", errors),
                    "BPAddin - Incorrect naming",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                EA.Project proj = cg.repo.GetProjectInterface();
                string pkgGuid = proj.GUIDtoXML(selectedPackage.PackageGUID);

                if (Directory.Exists(generatedDir)) {
                    foreach (string file in Directory.GetFiles(generatedDir, "*.cs"))
                        System.IO.File.Delete(file);
                }
                else
                {
                    Directory.CreateDirectory(generatedDir);
                }


                proj.GeneratePackage(pkgGuid, "");

                cg.initScreenDesigners(generatedDir);

                MessageBox.Show("EA generation complete.\n Files: " + generatedDir, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error during EA generation: \n\n" + ex.Message, "BPAddin - error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // put main screen as first into array of screens
            string mainScreen = cbxMainScreen.Text;
            List<string> screenNames = cg.screens.Select(el => el.Name).ToList();
            screenNames.Remove(mainScreen);
            screenNames.Insert(0, mainScreen);

            // build the prototype project
            builder.configure(generatedDir, uiLibDir, outputDir);
            builder.setScreenNames(screenNames);
            builder.setProgramNamespace(selectedPackage.Name);
            builder.buildProject();

            // close this window
            Close();
        }

        


    }
}
