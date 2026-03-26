using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class UITextBox : System.Windows.Forms.TextBox
    {
        public string placeholder;
        public UITextBox()
        {
            this.Width = 150;
            this.Height = 25;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
        }
        /*
        public string classname => "TextBox";
        public string ea_stereotype => "win32Edit";
        public string winforms_type => "System.Windows.Forms.TextBox";
        */
        public virtual void onChange() { }
    }
}
