using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class GroupBox : UI_Element
    {
        public GroupBox() : base("GroupBox", "win32GroupBox", "System.Windows.Forms.GroupBox") { }
    }
}
