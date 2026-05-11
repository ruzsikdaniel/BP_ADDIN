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
    }

    public class AddinClass : AddinBase
    {
        // menu elements here
        private const string menuHeader = "-&BPAddin";
        private const string menuGeneratePrototype = "&Generate Prototype";
        private const string menuSettings = "&Add-in Settings";

        private ProjectBuilder projectBuilder = new ProjectBuilder();

        private EA.Repository repo;

        // array of sub-menu elements for particular root menu element
        private List<string> menuItems = new List<string>();

        private bool projectOpened = false;

        public override void EA_FileOpen(Repository repository)
        { 
            repo = repository;

            string constr = repository.ConnectionString;
            string projectName = System.IO.Path.GetFileNameWithoutExtension(constr);

            MessageBox.Show("The project " + projectName + " has been opened successfully.");

            projectOpened = true;
        }

        public override object EA_GetMenuItems(Repository repository, string location, string menuName)
        {
            // initialize menu arrays
            // each menu array will contains names (string) of its sub-menu elements
            initMenuArrays();

            switch (menuName)
            {
                case "":
                    return menuHeader;
                case menuHeader:
                    return menuItems.ToArray();
                // each case is a root menu element
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
                case menuGeneratePrototype:
                    handleMenuGeneratePrototype();
                    break;
                case menuSettings:
                    handleMenuSettings();
                    break;
                default: return;
            }
        }


        private void initMenuArrays()
        {
            menuItems.Clear();

            menuItems.Add(menuGeneratePrototype);
            menuItems.Add(menuSettings);

            // add menu elements to different root menus
            // ...
        }

        private void handleMenuGeneratePrototype() {
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

        private void handleMenuSettings() {
            try
            {
                SettingsForm as_form = new SettingsForm(repo);
                as_form.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex.Message, "BPAddin - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //  in current form does not create new register values
        //  and thus does not register BPAddin automatically to EA COM API

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