using EA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{
    public class UILibraryInit
    {
        private const string UI_EL_STEREOTYPE = "UIElement";

        public void initUIElementStereotype(Repository repository)
        {
            object item;
            EA.ObjectType type = repository.GetContextItem(out item);
            if(type != EA.ObjectType.otPackage)
            {
                MessageBox.Show("Please choose a package in the Project Browser.");
                return;
            }

            EA.Package selected = (EA.Package)item;

                /*
            List<EA.Package> allPackages = new List<EA.Package>();
            foreach (EA.Package model in repository.Models)
            {
                collectPackages(model, allPackages);
            }

            EA.Package selected = showPackageSelector(allPackages);
                */
            if (selected == null)
            {
                MessageBox.Show("No package chosen.", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int count = initClassesPerPackage(selected);
            repository.RefreshModelView(selected.PackageID);

            MessageBox.Show(
                "Done! Stereotype <<UIElement>> was added to " + count + " classes.\n" +
                "Package: " + selected.Name,
                "BPAddin – Tag UI Library",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        }

        private int initClassesPerPackage(EA.Package pkg)
        {
            int count = 0;
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == "Class")
                {
                    el.Stereotype = UI_EL_STEREOTYPE;
                    el.Update();
                    count++;
                }
            }

            foreach (EA.Package sub in pkg.Packages)
            {
                count += initClassesPerPackage(sub);
            }

            return count;
        }

        private void collectPackages(EA.Package pkg, List<EA.Package> result)
        {
            result.Add(pkg);
            foreach (EA.Package sub in pkg.Packages)
            {
                collectPackages(sub, result);
            }
        }

        private EA.Package showPackageSelector(List<EA.Package> packages)
        {
            Form dialog = new Form();
            dialog.Text = "BPAddin – Vyber UI Library package";
            dialog.Width = 400;
            dialog.Height = 200;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MaximizeBox = false;

            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            lbl.Text = "Vyber package obsahujúci UI Library triedy:";
            lbl.Left = 10;
            lbl.Top = 15;
            lbl.Width = 360;
            dialog.Controls.Add(lbl);

            System.Windows.Forms.ComboBox combo = new System.Windows.Forms.ComboBox();
            combo.Left = 10;
            combo.Top = 40;
            combo.Width = 360;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var pkg in packages)
            {
                combo.Items.Add(pkg.Name + "  [ID: " + pkg.PackageID + "]");
            }
            if (combo.Items.Count > 0) combo.SelectedIndex = 0;
            dialog.Controls.Add(combo);

            System.Windows.Forms.Button btnOk = new System.Windows.Forms.Button();
            btnOk.Text = "OK";
            btnOk.Left = 200;
            btnOk.Top = 100;
            btnOk.Width = 80;
            btnOk.DialogResult = DialogResult.OK;
            dialog.Controls.Add(btnOk);

            System.Windows.Forms.Button btnCancel = new System.Windows.Forms.Button();
            btnCancel.Text = "Zrušiť";
            btnCancel.Left = 290;
            btnCancel.Top = 100;
            btnCancel.Width = 80;
            btnCancel.DialogResult = DialogResult.Cancel;
            dialog.Controls.Add(btnCancel);

            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            if (dialog.ShowDialog() == DialogResult.OK && combo.SelectedIndex >= 0)
            {
                return packages[combo.SelectedIndex];
            }

            return null;
        }

    }
}