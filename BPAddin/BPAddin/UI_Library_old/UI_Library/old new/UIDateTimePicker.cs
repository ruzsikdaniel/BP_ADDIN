using System;
using System.Drawing;

namespace BPAddin
{
    public class UIDateTimePicker : System.Windows.Forms.DateTimePicker
    {
        public UIDateTimePicker()
        {
            this.Width = 200;
            this.Height = 25;
            this.Font = new Font("Segoe UI", 9f);
        }
    }
}
