using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{
    public class UIComboBox : System.Windows.Forms.ComboBox
    {
        public UIComboBox()
        {
            this.Width = 150;
            this.Height = 25;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        /*
        public string classname => "ComboBox";
        public string ea_stereotype => "win32ComboBox";
        public string winforms_type => "System.Windows.Forms.ComboBox";
        */

        public virtual void onSelectionChanged() { }
    }
}
