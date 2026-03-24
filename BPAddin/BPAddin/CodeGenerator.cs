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

        private List<EAClass> cls = new List<EAClass>();
        private List<EA.Package> packages = new List<EA.Package>();
        private EA.Element uiScreen = null;
        private List<EA.Element> screens = new List<EA.Element>();


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

            string generatedDir = "C:\\_School\\BP\\funkcny_prototyp\\BP_ADDIN\\class_generation_output";
            string uiLibDir = "C:\\_School\\BP\\funkcny_prototyp\\BP_ADDIN\\BPAddin\\BPAddin\\UI_Library";
            string outputDir = "C:\\_School\\BP\\funkcny_prototyp\\project";

            // TODO: check if actually changes filepaths for class elements
            setPackageFilepaths(selectedPackage, generatedDir);

            initScreenClasses(repo, selectedPackage);

            initDesigners(generatedDir);

            try {
                EA.Project proj = repo.GetProjectInterface();
                string pkgGuid = proj.GUIDtoXML(selectedPackage.PackageGUID);
                proj.GeneratePackage(pkgGuid, "");
                MessageBox.Show("EA generation complete.\n Files: " + generatedDir, "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error during EA generation: \n\n" + ex.Message, "BPAddin - error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> screenNames = screens.Select(el => el.Name).ToList();

            ProjectBuilder builder = new ProjectBuilder(generatedDir, uiLibDir, outputDir);
            builder.setScreenNames(screenNames);
            builder.buildProject();

            Close();
        }

        private void setPartialToScreens(EA.Element el)
        {
            // skip if "partial" already exists
            foreach (EA.TaggedValue tagged in el.TaggedValues) {
                if (tagged.Name == "partial")
                    return;
            }

            // create new tagged value for partial keyword
            EA.TaggedValue tv = (EA.TaggedValue)el.TaggedValues.AddNew("partial", "");
                
            tv.Value = "true";
            tv.Update();

            el.TaggedValues.Refresh();
        }

        private void setInitConstructors(EA.Element el) {
            EA.Method constructor = null;
            foreach (EA.Method m in el.Methods)
            {
                if (m.Name == el.Name)
                {
                    constructor = m;    // save existing constructor for later rewriting
                    break;
                }
            }
            if (constructor == null)
            {
                // add new method into element
                constructor = (EA.Method)el.Methods.AddNew(el.Name, "");
                constructor.Update();
                el.Methods.Refresh();
            }
            // set internal code of method
            constructor.Code = "InitializeComponent();";
            constructor.Update();

            // save method and element changes
            el.Methods.Refresh();
            el.Update();
        }

        private void initScreenClasses(EA.Repository repo, EA.Package pkg)
        {
            findScreenElement(repo);
            if(this.uiScreen == null)
            {
                MessageBox.Show("Screen element not found.");
                return;
            }

            screens.Clear();
            initScreensArray(pkg);

            foreach (EA.Element el in screens)
            {
                setPartialToScreens(el);
                setInitConstructors(el);
            }
        }

        private void initScreensArray(EA.Package pkg) { 
            foreach(EA.Element el in pkg.Elements)
            {
                if(el.Type == "Class")
                {
                    foreach(EA.Connector connector in el.Connectors)
                    {
                        if((connector.Type == "Generalization" && connector.SupplierID == uiScreen.ElementID)){
                            screens.Add(el);
                            break;
                        }
                    }

                }
            }

            foreach(EA.Package sub in pkg.Packages)
            {
                initScreensArray(sub);
            }
        }

        private void initDesigners(string generatedDir) {

            UIComponentReader uireader = new UIComponentReader();
            DesignerBuilder dfbuilder = new DesignerBuilder();

            foreach (EA.Element screen in screens)
            {
                List<UIComponentInfo> components = uireader.getComponents(repo, screen);
                string designerContent = dfbuilder.build(screen.Name, components);

                string designerPath = Path.Combine(generatedDir, screen.Name + ".Designer.cs");

                System.IO.File.WriteAllText(designerPath, designerContent);
            }
        }



        private void findScreenElement(EA.Repository repo) {
            if (this.uiScreen != null)
                return;

            foreach (EA.Package model in repo.Models)
            {
                EA.Element el = findElementByName(model, "UIScreen");
                if (el != null)
                {
                    this.uiScreen = el;
                    return;
                }
            }
        }

        private EA.Element findElementByName(EA.Package pkg, string name) {
            foreach (EA.Element el in pkg.Elements) {
                if (el.Name == name)
                    return el;
            }
            
            foreach (EA.Package sub in pkg.Packages)
            {
                EA.Element el = findElementByName(sub, name);
                if(el != null)
                    return el;
            }
            return null;
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
                }
            }

            foreach (EA.Package sub in pkg.Packages) {
                setPackageFilepaths(sub, dir);
            }
        }
    }
}
