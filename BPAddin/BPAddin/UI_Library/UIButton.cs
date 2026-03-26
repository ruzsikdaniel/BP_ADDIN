using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class UIButton : System.Windows.Forms.Button
    {
        public UIButton()
        {
            this.Width = 100;
            this.Height = 30;
            this.BackColor = Color.LightGray;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.Text = "Button";
        }
        /*
        public string classname => "Button";
        public string ea_stereotype => "win32Button";
        public string winforms_type => "System.Windows.Forms.Button";*/

        public virtual void onClick() { }
    }
}