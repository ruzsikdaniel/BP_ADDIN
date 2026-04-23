using EA;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using static BPAddin.util.EABase;
using static BPAddin.util.EAMacros;
using BPAddin.Model;

namespace BPAddin
{
    public class CodeGenerator
    {
        public Repository repo;

        public EA.Element uiScreen; 
        public List<EA.Package> packages = new List<Package>();
        public List<EA.Element> screens = new List<EA.Element>();

        public CodeGenerator(EA.Repository repo)
        {
            this.repo = repo;
        }

        public void setInitConstructors(EA.Element el)
        {
            // initialize the constructor for an element
            if (el.Type != ELTYPE_CLASS)
                return;

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

        public void initScreenClasses(EA.Package pkg)
        {
            findScreenElement(repo);
            if (this.uiScreen == null)
            {
                MessageBox.Show("Screen element not found.");
                return;
            }

            screens.Clear();
            initScreensArray(pkg);

            foreach (EA.Element el in screens)
            {
                setTaggedValue(el, "partial", "true");  // (new) tagged value 'partial' - 'true'
                setInitConstructors(el);
            }
        }

        public void initScreensArray(EA.Package pkg)
        {
            // save all eligible screen classes into local array
            // - has type Class
            // - has Generalization to the UILibrary element UIScreen

            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == ELTYPE_CLASS)
                {
                    foreach (EA.Connector connector in el.Connectors)
                    {
                        if ((connector.Type == CONN_GENERALIZATION && connector.SupplierID == uiScreen.ElementID))
                        {
                            screens.Add(el);
                            break;
                        }
                    }

                }
            }

            foreach (EA.Package sub in pkg.Packages)
            {
                initScreensArray(sub);
            }
        }

        public void initScreenDesigners(string generatedDir)
        {
            // initialize the *.Designer.cs files of each screen

            UIComponentReader uireader = new UIComponentReader(repo);
            DesignerBuilder dfbuilder = new DesignerBuilder();

            foreach (EA.Element screen in screens)
            {
                List<string> methods = new List<string>();
                foreach(EA.Element child in screen.Elements)
                {
                    // save the screen's Activity diagram as its method
                    if(child.Type == ELTYPE_ACTIVITY)
                        methods.Add(child.Name);
                }

                List<UIComponentInfo> components = uireader.getComponents(repo, screen);


                string namespaceName = repo.GetPackageByID(screen.PackageID).Name;
                string designerContent = dfbuilder.build(screen.Name, components, namespaceName, methods);

                string designerPath = Path.Combine(generatedDir, screen.Name + ".Designer.cs");

                System.IO.File.WriteAllText(designerPath, designerContent);
            }
        }

        private void findScreenElement(EA.Repository repo)
        {
            if (this.uiScreen != null)
                return;

            foreach (EA.Package model in repo.Models)
            {
                EA.Element el = findElementByName(model, UILIB_SCREEN);
                if (el != null)
                {
                    this.uiScreen = el;
                    return;
                }
            }
        }

        

        public void setPackageFilepaths(EA.Package pkg, string dir)
        {
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == ELTYPE_CLASS)
                {
                    string filepath = Path.Combine(dir, el.Name + ".cs");
                    el.Gentype = GENTYPE_CSHARP;
                    el.Genfile = filepath;
                    el.Update();
                }
            }

            foreach (EA.Package sub in pkg.Packages)
            {
                setPackageFilepaths(sub, dir);
            }
        }

    }
}
