using System;
using System.Drawing;

namespace BPAddin
{
    public class UITreeView : System.Windows.Forms.TreeView
    {
        public UITreeView()
        {
            this.Width = 200;
            this.Height = 150;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
        }

        public virtual void onAfterSelect() { }
    }
}
