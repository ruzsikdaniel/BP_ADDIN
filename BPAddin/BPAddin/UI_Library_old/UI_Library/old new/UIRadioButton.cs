using System;
using System.Drawing;

namespace BPAddin
{
    public class UIRadioButton : System.Windows.Forms.RadioButton
    {
        public UIRadioButton()
        {
            this.Width = 120;
            this.Height = 25;
            this.Text = "RadioButton";
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.Checked = false;
        }

        public virtual void onCheckedChanged() { }
    }
}
