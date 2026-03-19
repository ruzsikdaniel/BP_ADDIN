using System;
using System.Drawing;

namespace BPAddin
{
    public class UIGroupBox : System.Windows.Forms.GroupBox
    {
        public UIGroupBox()
        {
            this.Width = 200;
            this.Height = 150;
            this.Text = "GroupBox";
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
        }
    }
}
