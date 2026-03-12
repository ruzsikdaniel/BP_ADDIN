using BPAddin.UI_Library;
using BPAddin.UI_LIbrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class CheckBox : System.Windows.Forms.CheckBox
    {
        public CheckBox() {
            this.Width = 120;
            this.Height = 25;
            this.Text = "CheckBox";
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.Checked = false;
        }

        /*
        public string classname => "CheckBox";
        public string ea_stereotype => "win32CheckBox";
        public string winforms_type => "System.Windows.Forms.CheckBox";
        */

        public virtual void onCheckedChanged() { }
    }
}
