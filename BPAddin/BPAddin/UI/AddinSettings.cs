using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BPAddin
{
    public partial class SettingsForm : Form
    {
        private struct DirSetting
        {

            // change structure to accomodate different input types, not just TextBox (combobox)
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

            dirSettings.Add(new DirSetting {
                tbx = tbxGenerated,
                description = "Select Generated Source Directory",
                get = () => Properties.Settings.Default.GeneratedDir,
                set = v => Properties.Settings.Default.GeneratedDir = v
            });

            dirSettings.Add(new DirSetting
            {
                tbx = tbxUILib,
                description = "Select UI Library Directory",
                get = () => Properties.Settings.Default.UILibraryDir,
                set = v => Properties.Settings.Default.UILibraryDir = v
            });

            dirSettings.Add(new DirSetting
            {
                tbx = tbxProject,
                description = "Select Project Directory",
                get = () => Properties.Settings.Default.ProjectDir,
                set = v => Properties.Settings.Default.ProjectDir = v
            });

            dirSettings.Add(new DirSetting
            {
                tbx = tbxApplicationCDPkg,
                description = "",
                get = () => Properties.Settings.Default.AppCDPkg,
                set = v => Properties.Settings.Default.AppCDPkg = v
            });

            dirSettings.Add(new DirSetting
            {
                tbx = tbxUIDiagramPkg,
                description = "",
                get = () => Properties.Settings.Default.UIDiagramPkg,
                set = v => Properties.Settings.Default.UIDiagramPkg = v
            });

            initTbx();
        }
        private void initTbx()
        {
            foreach(var s in dirSettings)
            {
                s.tbx.Text = cleanDoubleBacklines(s.get());
            }
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

                    //MessageBox.Show(s.description + " changed to: " + choice);
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

        

        private void btnUILib_Click(object sender, EventArgs e)
        {
            browseDirSetting(dirSettings[1]);
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            browseDirSetting(dirSettings[2]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (var s in dirSettings) { 
                s.set(s.tbx.Text);
            }

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
            string uiDiagramText = tbxUIDiagramPkg.Text;
            string appCDText = tbxApplicationCDPkg.Text;
            uisync.syncAll(uiDiagramText, appCDText);
        }

        private void btnImportUILib_Click(object sender, EventArgs e)
        {

        }
    }
}
