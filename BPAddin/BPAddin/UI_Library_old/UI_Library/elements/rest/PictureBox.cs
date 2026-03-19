using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class PictureBox : UI_Element
    {
        public PictureBox() : base("PictureBox", "win32Picture", "System.Windows.Forms.PictureBox") { }
    }
}
