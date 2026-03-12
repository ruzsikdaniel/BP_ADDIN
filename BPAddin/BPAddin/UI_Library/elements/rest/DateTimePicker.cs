using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class DateTimePicker : UI_Element
    {
        public DateTimePicker() : base("DateTimePicker", "win32DateTime", "System.Windows.Forms.DateTimePicker") { }
    }
}
