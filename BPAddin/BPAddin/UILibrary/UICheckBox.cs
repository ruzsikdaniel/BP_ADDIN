using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class UICheckBox : System.Windows.Forms.CheckBox
    {
        public UICheckBox()
        {
            this.Width = 120;
            this.Height = 25;
            this.Text = "CheckBox";
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.Checked = false;
            this.CheckedChanged += OnCheckedChanged;
        }
        protected virtual void OnCheckedChanged(object sender, EventArgs e) { OnCheckedChanged(); }
        protected virtual void OnCheckedChanged() { }
    }
}
