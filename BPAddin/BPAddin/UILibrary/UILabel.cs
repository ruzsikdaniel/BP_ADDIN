using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class UILabel : System.Windows.Forms.Label
    {
        public UILabel()
        {
            this.Width = 100;
            this.Height = 20;
            this.Text = "Label";
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
        }
    }
}