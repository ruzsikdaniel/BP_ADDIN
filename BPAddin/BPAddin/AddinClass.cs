using BPAddin.Util;
using EA;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using static BPAddin.util.EABase;

namespace BPAddin
{   public abstract class AddinBase
    {
        public virtual object EA_GetMenuItems(Repository repository, string location, string menuName) { return null; }
        public virtual void EA_MenuClick(Repository repository, string location, string menuName, string itemName) { }
        public virtual void EA_FileOpen(Repository repository) { }

        // not a valid EA event
        public virtual void EA_OnPostSaveDiagram(Repository repository, int diagramID) { }

        public virtual bool EA_OnPostNewElement(EA.Repository repository, EA.EventProperties info) { return false; }
    }

    public class AddinClass : AddinBase
    {
        // menu elements here
        private const string menuHeader = "-&BPAddin";
        private const string menuGenerateCode = "&Generate Code";
        private const string menuStereotypeInit = "&Initialize UI Library Stereotypes";
        private const string menuSettings = "&Add-in Settings";

        private ProjectBuilder projectBuilder = new ProjectBuilder();

        private EA.Repository repo;

        // array of sub-menu elements for particular root menu element
        private List<string> menuItems = new List<string>();

        // state variables
        private bool projectOpened = false;

        public override void EA_FileOpen(Repository repository)
        {
            repo = repository;

            string constr = repository.ConnectionString;
            string projectName = System.IO.Path.GetFileNameWithoutExtension(constr);

            MessageBox.Show("The project " + projectName + " has been opened successfully.");

            UIModelSync uiModelSync = new UIModelSync(repository);

            string uiPkgName = Properties.Settings.Default.UIDiagramPkg;
            string appPkgName = Properties.Settings.Default.AppCDPkg;

            uiModelSync.syncAll(uiPkgName, appPkgName);

            projectOpened = true;
        }

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
                    return menuItems.ToArray();
                // each case here represents a root menu element
                default:
                    return null;
            }
        }

        public override void EA_MenuClick(Repository repository, string location, string menuName, string itemName)
        {
            if (!projectOpened) {
                MessageBox.Show("The project is not opened!", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // if possible, in the future reformat this into a dictionary of menu elements and handler function
            // let appropriate handler functions be called in the case of particular menu elements
            switch (itemName)
            {
                case menuGenerateCode:
                    handleMenuGenerateCode();
                    break;
                case menuStereotypeInit:
                    handleMenuStereotypeInit();
                    break;
                case menuSettings:
                    handleMenuSettings();
                    break;
                default: return;
            }
        }

        public override void EA_OnPostSaveDiagram(Repository repository, int diagramID)
        {
            EA.Diagram diagram = repository.GetDiagramByID(diagramID);
            //if (diagram.Type != "Win32 User Interface") 
            //    return;

            MessageBox.Show("Diagram saved!");
        }

        public override bool EA_OnPostNewElement(Repository repository, EventProperties info)
        {
            // fires when a new element is created
            int elementID = int.Parse(info.Get("ElementId").Value.ToString());
            EA.Element el = repository.GetElementByID(elementID);
            //MessageBox.Show("New Element: " + el.Name + ", Stereotype: " + el.Stereotype);
            return false;
        }


        private void initMenuArrays()
        {
            menuItems.Clear();

            menuItems.Add(menuGenerateCode);
            //menus_menuHeader.Add(menuStereotypeInit);
            menuItems.Add(menuSettings);

            // add menu elements to different root menus
            // ...
        }

        private void handleMenuGenerateCode() {
            try
            {
                List<EA.Package> packages = findAllPackages(repo);

                CodeGeneratorForm form = new CodeGeneratorForm(repo, projectBuilder);
                form.setLblText("Choose a package containing classes for generating:");
                form.setLblMainScreen("Choose the prototype's main screen:");
                form.initComboboxes(packages);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex.Message, "BPAddin - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void handleMenuStereotypeInit() {
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

        private void handleMenuSettings() { 
            // menuSettings menu logic

            try
            {
                SettingsForm as_form = new SettingsForm(repo);
                // init of text boxes
                as_form.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex.Message, "BPAddin - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        [System.Runtime.InteropServices.ComRegisterFunction]
        public static void registerFunction(Type t)
        {
            // register the addin for the 64-bit EA Add-in versions
            Microsoft.Win32.Registry.CurrentUser
                .CreateSubKey(@"Software\Sparx Systems\EAAddins64\BPAddin")
                .SetValue("", "BPAddin.AddinClass");


            // register the addin for the 32-bit EA Add-in versions
            Microsoft.Win32.Registry.CurrentUser
               .CreateSubKey(@"Software\Sparx Systems\EAAddins\BPAddin")
               .SetValue("", "BPAddin.AddinClass");
        }

        [System.Runtime.InteropServices.ComUnregisterFunction]
        public static void unregisterFunction(Type t)
        {
            Microsoft.Win32.Registry.CurrentUser
                .DeleteSubKey(@"Software\Sparx Systems\EAAddins64\BPAddin", false);
        }
    }
}