using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{
    public class UIComboBox : System.Windows.Forms.ComboBox
    {
        public UIComboBox()
        {
            this.Width = 150;
            this.Height = 25;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Font = new Font("Segoe UI", 9f);
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectedIndexChanged += OnSelectedIndexChanged;
            this.TextChanged += OnTextChanged;
        }
        protected virtual void OnSelectedIndexChanged(object sender, EventArgs e) { OnSelectedIndexChanged(); }
        protected virtual void OnSelectedIndexChanged() { }

        protected virtual void OnTextChanged(object sender, EventArgs e) { OnTextChanged(); }
        protected virtual void OnTextChanged() { }
    }
}
