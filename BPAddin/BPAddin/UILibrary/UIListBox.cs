using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class UIListBox : System.Windows.Forms.ListBox
    {
        public UIListBox()
        {
            this.Width = 200;
            this.Height = 150;
            this.Font = new Font("Segoe UI", 9f);
        }
    }
}
