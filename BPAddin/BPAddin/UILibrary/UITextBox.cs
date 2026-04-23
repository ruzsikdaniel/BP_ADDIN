using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class UITextBox : System.Windows.Forms.TextBox
    {
        public string placeholder;
        public UITextBox()
        {
            this.Width = 150;
            this.Height = 25;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.TextChanged += OnTextChanged;
            this.Leave += OnLeave;
            this.KeyDown += OnKeyDown
                ;
        }

        protected virtual void OnTextChanged(object sender, EventArgs e) { OnTextChanged(); }
        protected virtual void OnTextChanged() { }

        protected virtual void OnLeave(object sender, EventArgs e) { OnLeave(); }
        protected virtual void OnLeave() { }

        protected virtual void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) { OnKeyDown(e); }
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e) { }
    }
}
