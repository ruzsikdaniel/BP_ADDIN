using EA;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{

    public class CodeGenerator
    {
        public Repository repo;

        public List<EAClass> cls = new List<EAClass>();
        public List<Package> packages = new List<EA.Package>();
        public EA.Element uiScreen = null;
        public List<EA.Element> screens = new List<EA.Element>();

        public CodeGenerator(EA.Repository repo)
        {
            this.repo = repo;
        }

        public void setPartialToScreens(EA.Element el)
        {
            // skip if "partial" already exists
            foreach (EA.TaggedValue tagged in el.TaggedValues)
            {
                if (tagged.Name == "partial")
                    return;
            }

            // create new tagged value for partial keyword
            EA.TaggedValue tv = (EA.TaggedValue)el.TaggedValues.AddNew("partial", "");

            tv.Value = "true";
            tv.Update();

            el.TaggedValues.Refresh();
        }

        public void setInitConstructors(EA.Element el)
        {
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
                setPartialToScreens(el);
                setInitConstructors(el);
            }
        }

        public void initScreensArray(EA.Package pkg)
        {
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == "Class")
                {
                    foreach (EA.Connector connector in el.Connectors)
                    {
                        if ((connector.Type == "Generalization" && connector.SupplierID == uiScreen.ElementID))
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

        public void initDesigners(string generatedDir)
        {

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

        private void findScreenElement(EA.Repository repo)
        {
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

        private EA.Element findElementByName(EA.Package pkg, string name)
        {
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Name == name)
                    return el;
            }

            foreach (EA.Package sub in pkg.Packages)
            {
                EA.Element el = findElementByName(sub, name);
                if (el != null)
                    return el;
            }
            return null;
        }

        public void setPackageFilepaths(EA.Package pkg, string dir)
        {
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == "Class")
                {
                    string filepath = Path.Combine(dir, el.Name + ".cs");
                    el.Gentype = "C#";
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
