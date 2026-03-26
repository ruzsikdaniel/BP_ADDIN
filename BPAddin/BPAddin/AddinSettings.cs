using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public SettingsForm()
        {
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

                    MessageBox.Show(s.description + " changed to: " + choice);
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
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
