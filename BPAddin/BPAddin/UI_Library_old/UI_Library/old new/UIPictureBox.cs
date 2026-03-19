using System;
using System.Drawing;
using System.Windows.Forms;

namespace BPAddin
{
    public class UIPictureBox : System.Windows.Forms.PictureBox
    {
        public UIPictureBox()
        {
            this.Width = 100;
            this.Height = 100;
            this.BackColor = Color.LightGray;
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
