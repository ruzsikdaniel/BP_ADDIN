using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class Spinner : UI_Element
    {
        public Spinner() : base("Spinner", "win32Spin", "System.Windows.Forms.NumericUpDown") { }
    }
}
