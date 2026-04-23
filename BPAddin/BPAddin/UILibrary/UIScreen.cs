using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{
    public class UIScreen : System.Windows.Forms.Form
    {
        public string title;
        public UIScreen()
        {
            this.Width = 800;
            this.Height = 600;
            this.BackColor = Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += OnLoad;
            this.FormClosing += OnFormClosing;
        }


        protected virtual void OnLoad(object sender, EventArgs e) { OnLoad(); }
        protected virtual void OnLoad() { }

        protected virtual void OnFormClosing(object sender, FormClosingEventArgs e) { OnFormClosing(e); }
        protected override void OnFormClosing(FormClosingEventArgs e) { }


        public new void Show() { base.Show(); }
        public new void Hide() { base.Hide(); }
        public new void Close() { base.Close(); }
        public new DialogResult ShowDialog() { return base.ShowDialog(); }
    }
}
