using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPAddin.UI_Library;

namespace BPAddin
{
    public class Slider : UI_Element
    {
        public Slider() : base("Slider", "win32Slider", "System.Windows.Forms.TrackBar") { }
    }
}
