using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class ListBox : UI_Element
    {
        public ListBox() : base("ListBox", "win32ListBox", "System.Windows.Forms.ListBox") { }
    }
}
