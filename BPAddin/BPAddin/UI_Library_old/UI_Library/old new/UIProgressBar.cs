using System;
using System.Drawing;

namespace BPAddin
{
    public class UIProgressBar : System.Windows.Forms.ProgressBar
    {
        public UIProgressBar()
        {
            this.Width = 200;
            this.Height = 25;
            this.Minimum = 0;
            this.Maximum = 100;
            this.Value = 0;
        }
    }
}
