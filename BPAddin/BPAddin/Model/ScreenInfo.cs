using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin.model
{
    public class ScreenInfo
    {
        public string clsName { get; set; }
        public string pkgName { get; set; }
        public EA.Package pkg { get; set; }
    }
}
