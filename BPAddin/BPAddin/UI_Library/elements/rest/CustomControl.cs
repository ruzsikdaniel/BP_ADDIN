using BPAddin.UI_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class CustomControl : UI_Element
    {
        public CustomControl() : base("CustomControl", "win32CustomControls", "System.Windows.Forms.UserControl") { }
    }
}
