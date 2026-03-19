using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BPAddin.UI_Library;
using BPAddin.UI_LIbrary;

namespace BPAddin
{
    public class Screen: System.Windows.Forms.Form
    {
        public string title;
        public Screen(){
            this.Width = 800;
            this.Height = 600;
            this.BackColor = Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        /*
        public string classname => "Screen";
        public string ea_stereotype => "win32Dialog";
        public string winforms_type => "System.Windows.Forms.Form";
        */
    }
}
