using System;
using System.Collections.Generic;
using System.Windows.Forms;

using static BPAddin.util.EABase;
using BPAddin.Util;

namespace BPAddin
{
    public partial class SettingsForm : Form
    {
        private struct DirSetting
        {
            public TextBox tbx;
            public string description;
            public Func<string> get;
            public Action<string> set;
        }

        private List<DirSetting> dirSettings;
        private EA.Repository repo;

        public SettingsForm(EA.Repository repository)
        {
            repo = repository;
            InitializeComponent();
            initDirSettings();
        }

        private void initDirSettings()
        {
            this.dirSettings = new List<DirSetting>();

            tbxUILib.Text = AddinPaths.UILibraryPath;
            tbxUILib.Enabled = false;

            dirSettings.Add(new DirSetting {
                tbx = tbxGenerated,
                description = "Select Generated Source Directory",
                get = () => string.IsNullOrEmpty(Properties.Settings.Default.GeneratedDir) 
                    ? AddinPaths.DefaultGeneratedDir 
                    : Properties.Settings.Default.GeneratedDir,
                set = v => Properties.Settings.Default.GeneratedDir = v
            });

            dirSettings.Add(new DirSetting
            {
                tbx = tbxProject,
                description = "Select Project Directory",
                get = () => string.IsNullOrEmpty(Properties.Settings.Default.ProjectDir)
                    ? AddinPaths.DefaultProjectDir
                    : Properties.Settings.Default.ProjectDir,
                set = v => Properties.Settings.Default.ProjectDir = v
            });

            initTbx();
            initPackageCbx();
        }
        private void initTbx()
        {
            foreach(var s in dirSettings)
            {
                s.tbx.Text = cleanDoubleBacklines(s.get());
            }
        }

        private void initPackageCbx() {
            List<EA.Package> packages = new List<EA.Package>();

            cbxAppCDPkg.Items.Clear();
            cbxUIDiagramPkg.Items.Clear();

            packages.AddRange(findAllPackages(repo));

            foreach (EA.Package pkg in packages)
            {
                cbxAppCDPkg.Items.Add(pkg.Name);
                cbxUIDiagramPkg.Items.Add(pkg.Name);
            }

            cbxAppCDPkg.Text = Properties.Settings.Default.AppCDPkg;
            cbxUIDiagramPkg.Text = Properties.Settings.Default.UIDiagramPkg;
        }

        private void browseDirSetting(DirSetting s)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                string path = s.tbx.Text;

                dlg.Description = s.description;

                if (string.IsNullOrEmpty(s.tbx.Text))
                    path = s.get();
                dlg.SelectedPath = path;

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    string choice = dlg.SelectedPath;   // save the choice

                    s.tbx.Text = choice;    // update appropriate textbox
                    s.set(choice);          // set appropriate setting to choice value
                }
            }
        }

        private string cleanDoubleBacklines(string text)
        {
            if (text == null)
                return string.Empty;

            string result = text.Replace("\\\\", "\\");

            return result;
        }

        private void btnGenerated_Click(object sender, EventArgs e)
        {
            browseDirSetting(dirSettings[0]);
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            browseDirSetting(dirSettings[1]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (var s in dirSettings) { 
                s.set(s.tbx.Text);
            }

            Properties.Settings.Default.AppCDPkg = cbxAppCDPkg.Text;
            Properties.Settings.Default.UIDiagramPkg = cbxUIDiagramPkg.Text;

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UIModelSync uisync = new UIModelSync(repo);
            string uiDiagramText = cbxUIDiagramPkg.Text;
            string appCDText = cbxAppCDPkg.Text;

            uisync.syncAll(uiDiagramText, appCDText);
        }

        private void btnImportUILib_Click(object sender, EventArgs e)
        {
            string uiLibPath = AddinPaths.UILibraryPath;

            if (!System.IO.Directory.Exists(uiLibPath) || System.IO.Directory.GetFiles(uiLibPath, "UI*.cs").Length == 0) {
                MessageBox.Show("UI Library not found at path: \n" + uiLibPath +
                    "\n\nPlease reinstall BPAddin.",
                    "BPAddin - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Importer imp = new Importer(repo);
                imp.initUILibrary(uiLibPath);
                MessageBox.Show("UI Library imported successfully.", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error during import:\n\n" + ex.Message, "BPAddin - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
