using System;
using System.Drawing;
using System.Windows.Forms;

namespace BPAddin
{
    public class UIListView : System.Windows.Forms.ListView
    {
        public UIListView()
        {
            this.Width = 200;
            this.Height = 150;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.View = View.Details;
            this.FullRowSelect = true;
            this.GridLines = true;
        }
    }
}
