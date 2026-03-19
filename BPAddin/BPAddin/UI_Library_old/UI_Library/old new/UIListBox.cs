using System;
using System.Drawing;

namespace BPAddin
{
    public class UIListBox : System.Windows.Forms.ListBox
    {
        public UIListBox()
        {
            this.Width = 150;
            this.Height = 100;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
        }

        public virtual void onSelectedIndexChanged() { }
    }
}
