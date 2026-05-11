using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin
{
    public class StereotypeMap
    {
        // map of Win32 UI diagram - UI Library elements
        public Dictionary<string, string> map = new Dictionary<string, string>() {
            { "win32Dialog", "UIScreen"},
            { "win32Button", "UIButton"},
            { "win32CheckBox", "UICheckBox"},
            { "win32Edit", "UITextBox"},
            { "win32ComboBox", "UIComboBox"},
            { "win32ListBox", "UIListBox"},
            { "win32GroupBox", "UIGroupBox"},
            { "win32RadioButton", "UIRadioButton"},
            { "win32StaticText", "UILabel"},
            { "win32PictureBox", "UIPictureBox"},
            { "win32ProgressBar", "UIProgressBar"},
            { "win32ListControl", "UIListView"},
            { "win32TreeControl", "UITreeView"},
            { "win32TabControl", "UITabControl"},
            { "win32DateTimePicker", "UIDateTimePicker"},
        };


        public static string getClassPrefix(string stereotype, Dictionary<string, string> map)
        {
            if (!map.ContainsKey(stereotype))
                return null;

            string uiName = map[stereotype];                // UIScreen
            string rawName = uiName.Substring(2);           // Screen
            rawName = char.ToLower(rawName[0]) + rawName.Substring(1);  // screen
            return rawName;
        }
    }
}
