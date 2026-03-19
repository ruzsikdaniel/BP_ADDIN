using System;
using System.Drawing;

namespace BPAddin
{
    public class UITabControl : System.Windows.Forms.TabControl
    {
        public UITabControl()
        {
            this.Width = 300;
            this.Height = 200;
            this.Font = new Font("Segoe UI", 9f);
        }
    }
}
