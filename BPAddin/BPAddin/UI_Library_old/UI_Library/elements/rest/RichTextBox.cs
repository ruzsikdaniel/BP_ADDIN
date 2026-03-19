using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class RichTextBox : UI_Element
    {
        public RichTextBox() : base("RichTextBox", "win32RichEdit", "System.Windows.Forms.RichTextBox") { }
    }
}
