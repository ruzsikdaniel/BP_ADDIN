using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin.UI_Library
{
    public abstract class UI_Element
    {
        public string classname;       // UI Library class name
        public string ea_stereotype;   // EA UI Diagram stereotype name
        public string winforms_type;

        public string filepath;                 // path for specific element file
        public static string basedir = "";      // fill - main directory of UI_Library

        // for now use these base attributes
        public float width;
        public float height;
        public int x;
        public int y;
        public string bgColor;
        public string txtColor;
        public float fontSize;
        public string text;

        public UI_Element(string classname, string ea_stereotype, string winforms_type)
        {
            this.classname = classname;
            this.ea_stereotype = ea_stereotype;
            this.winforms_type = winforms_type;
            this.filepath = System.IO.Path.Combine(basedir, classname + ".cs");

            // globally default values for now
            this.width = 100;
            this.height = 30;
            this.x = 0;
            this.y = 0;
            this.bgColor = "#FFFFFF";
            this.txtColor = "#000000";
            this.fontSize = 12;
            this.text = "";
        }
    }
}
