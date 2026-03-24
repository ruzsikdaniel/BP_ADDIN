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

    public class AddinClass : AddinBase
    {
        // menu elements here
        private const string menuHeader = "-&TestGenerate";
        private const string menuGenerateCode = "&Generate Code";
        private const string menuStereotypeInit = "&Initialize UI Library Stereotypes";
        private const string menuSettings = "&Add-in Settings";

        // array of sub-menu elements for particular root menu element
        private List<string> menus_menuHeader = new List<string>();

        // state variables
        private bool projectOpened = false;

        public override object EA_GetMenuItems(Repository repository, string location, string menuName)
        {
            // initialize menu arrays
            // each menu array will contains names of its sub-menu elements
            initMenuArrays();

            switch (menuName)
            {
                case "":
                    return menuHeader;
                case menuHeader:
                    return menus_menuHeader.ToArray();
                // each case here represents a root menu element
                default:
                    return null;
            }
        }

        public override void EA_MenuClick(Repository repo, string location, string menuName, string itemName)
        {
            if (!projectOpened) {
                MessageBox.Show("Projekt nie je otvorený.", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: reformat this into a dictionary of menu elements and handler function
            // let appropriate handler functions be called in the case of particular menu elements
            switch (itemName)
            {
                case menuGenerateCode:
                    handleMenuGenerateCode(repo);
                    break;
                case menuStereotypeInit:
                    handleMenuStereotypeInit(repo);
                    break;
                case menuSettings:
                    handleMenuSettings(repo);
                    break;
                default: return;
            }
        }

        public override void EA_FileOpen(Repository repository)
        {
            string constr = repository.ConnectionString;
            string projectName = System.IO.Path.GetFileNameWithoutExtension(constr);

            MessageBox.Show("The project " + projectName + " has been opened successfully.");
            projectOpened = true;
        }

        private void initMenuArrays()
        {
            // menu_menuHeader - main menu
            clearMenuArray(menus_menuHeader);
            menus_menuHeader.Add(menuGenerateCode);
            menus_menuHeader.Add(menuStereotypeInit);
            menus_menuHeader.Add(menuSettings);

            // add menu elements to different root menus
            // ...
        }

        private void clearMenuArray(List<string> menuArray)
        {
            menuArray.Clear();
        }

        private void handleMenuGenerateCode(EA.Repository repo) {
            try
            {
                PackageFinder pf = new PackageFinder();
                List<EA.Package> packages = pf.get_all_packages(repo);

                CodeGenerator form = new CodeGenerator(repo);
                form.setLblText("Choose a package containing classes for generating:");
                form.initPackages(packages);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex.Message, "BPAddin - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void handleMenuStereotypeInit(EA.Repository repo) {
            try
            {
                UILibraryInit init = new UILibraryInit();
                init.initUIElementStereotype(repo);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error during initialization process:\n\n" + e.Message, "BPAddin - error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void handleMenuSettings(EA.Repository repo) { 
            // menuSettings menu logic


        }

    }

    public class PackageFinder
    {
        public List<EA.Package> get_all_packages(Repository repo) { 
            List<EA.Package> result = new List<EA.Package>();

            foreach (EA.Package model in repo.Models)
                collect_packages(model, result);
            return result;
        }

        private void collect_packages(EA.Package pkg, List<EA.Package> result) { 
            result.Add(pkg);
            foreach(EA.Package sub in pkg.Packages)
                collect_packages(sub, result);
        }

    }
}