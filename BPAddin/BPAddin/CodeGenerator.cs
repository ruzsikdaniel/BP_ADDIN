using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;

namespace BPAddin
{
    public partial class CodeGenerator : Form
    {
        private EA.Repository repo;

        private EAClass chosenClass = new EAClass();
        private List<EAClass> cls = new List<EAClass>();

        public CodeGenerator(EA.Repository repository)
        {
            InitializeComponent();
            repo = repository;
        }

        public void setLblText(string text){
            lblText.Text = text;
        }

        public void initClasses(List<EAClass> classes){
            cbxClasses.Items.Clear();
            
            cls.Clear();
            cls.AddRange(classes);

            foreach (EAClass c in classes) {
                cbxClasses.Items.Add(c.clsName);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int index = cbxClasses.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please choose a class.");
                return;
            }

            EA.Project project = repo.GetProjectInterface();
            string pkg_guid = project.GUIDtoXML(chosenClass.pkg.PackageGUID);

            project.GeneratePackage(pkg_guid, "");

            MessageBox.Show("Code generated for class: " + chosenClass.clsName);
            Close();
        }
    }
}
