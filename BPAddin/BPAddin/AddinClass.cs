using EA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{   public abstract class AddinBase
    {
        public virtual object EA_GetMenuItems(Repository repository, string location, string menuName) { return null; }
        public virtual void EA_MenuClick(Repository repository, string location, string menuName, string itemName) { }
        public virtual void EA_FileOpen(Repository repository) { }
    }

    public class EAClass
    {
        public string clsName { get; set; }
        public string pkgName { get; set; }
        public EA.Package pkg { get; set; }
    }

    public class AddinClass : AddinBase
    {
        private const string menuHeader = "-&TestGenerate";

        private const string menuGenerateCode = "&Generate Code";

        private string message = "This is a sample text.";

        private bool projectOpened = false;

        public override object EA_GetMenuItems(Repository repository, string location, string menuName)
        {
            if (menuName == "")
                return menuHeader;

            if (menuName == menuHeader)
                return new string[] { menuGenerateCode };

            return null;
        }

        public override void EA_MenuClick(Repository repository, string location, string menuName, string itemName)
        {
            if(itemName == menuGenerateCode)
            {
                //message = "Generation clicked!";
                //MessageBox.Show(message);

                if (projectOpened){ 
                    ClassFinder cf = new ClassFinder();
                    List<EAClass> classes = cf.getClassNames(repository);

                    CodeGenerator form = new CodeGenerator(repository);

                    form.setLblText("Choose a class to generate source code for.");
                    form.initClasses(classes);
                    form.ShowDialog();
                }
            }
        }

        public override void EA_FileOpen(Repository repository)
        {
            string constr = repository.ConnectionString;
            string projectName = System.IO.Path.GetFileNameWithoutExtension(constr);

            MessageBox.Show("The project " + projectName + " has been opened successfully.");
            projectOpened = true;
        }
    }

    public class ClassFinder {

        public List<EAClass> getClassNames(Repository repository)
        { 
            List<EAClass> classes = new List<EAClass>();

            foreach (EA.Package model in repository.Models)
            {
                findClasses(model, classes);
            }

            return classes;
        }

        private void findClasses(Package pkg, List<EAClass> classes)
        {
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == "Class")
                {
                    classes.Add(new EAClass
                    {
                        clsName = el.Name,
                        pkgName = pkg.Name,
                        pkg = pkg
                    });
                }
            }

            foreach (EA.Package subPkg in pkg.Packages)
            {
                findClasses(subPkg, classes);
            }
        }
    }
}