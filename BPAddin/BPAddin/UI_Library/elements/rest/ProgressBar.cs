using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class ProgressBar : UI_Element
    {
        public ProgressBar() : base("ProgressBar", "win32Progress", "System.Windows.Forms.ProgressBar") { }
    }
}
