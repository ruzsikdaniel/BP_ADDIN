using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BPAddin.Model;
using EA;

namespace BPAddin
{
    public static class EventMap {
        public static Dictionary<string, (string eventName, string delegateType)> componentEvents = new Dictionary<string, (string, string)>{
            { "UIButton", ("Click", "System.EventHandler") },
            { "UIListBox", ("SelectedIndexChanged", "System.EventHandler") },
            { "UITextBox", ("TextChanged", "System.EventHandler") },
            { "UICheckBox", ("CheckedChanged", "System.EventHandler") },
            { "UIComboBox", ("SelectedIndexChanged", "System.EventHandler") }
        };
    }

    public class DesignerBuilder
    {
        public string build(string screenName, List<UIComponentInfo> components, string namespaceName, List<String> screenMethods)
        {
            var scrSize = components.FirstOrDefault(c => c.name == "__screen__");
            var comps = components.Where(c => c.name != "__screen__").ToList();

            var sb = new StringBuilder();

            // 0) structure
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Drawing;");
            sb.AppendLine("using System.Windows.Forms;");
            sb.AppendLine();
            sb.AppendLine("namespace " + namespaceName);
            sb.AppendLine("{");
            sb.AppendLine("partial class " + screenName);
            sb.AppendLine("{");
            sb.AppendLine("    private System.ComponentModel.IContainer components = null;");
            sb.AppendLine();
            sb.AppendLine("    protected override void Dispose(bool disposing)");
            sb.AppendLine("    {");
            sb.AppendLine("        if (disposing && (components != null))");
            sb.AppendLine("            components.Dispose();");
            sb.AppendLine("        base.Dispose(disposing);");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    private void InitializeComponent()");
            sb.AppendLine("    {"); 

            // 1) instances of screen components
            foreach (var c in comps) { 
                sb.AppendLine("        this." + c.name + " = new " + c.name + "();");
                appendHandler(sb, c, screenMethods);     
            }
            sb.AppendLine("        this.SuspendLayout();");
            sb.AppendLine();

            // 2) configuration for each screen component
            foreach (var c in comps)
            {
                sb.AppendLine("        //");
                sb.AppendLine("        // " + c.name);
                sb.AppendLine("        //");
                sb.AppendLine("        this." + c.name + ".Name = \"" + c.name + "\";");
                sb.AppendLine("        this." + c.name + ".Text = \"" + c.text + "\";");
                sb.AppendLine("        this." + c.name + ".Location = new System.Drawing.Point(" + c.x + ", " + c.y + ");");
                sb.AppendLine("        this." + c.name + ".Size = new System.Drawing.Size(" + c.w + ", " + c.h + ");");
                sb.AppendLine("        this.Controls.Add(this." + c.name + ");");
                sb.AppendLine();
            }

            // 3) configuration for main screen 
            sb.AppendLine("        //");
            sb.AppendLine("        // " + screenName);
            sb.AppendLine("        //");
            sb.AppendLine("        this.Text = \"" + screenName + "\";");
            sb.AppendLine("        this.ClientSize = new System.Drawing.Size(" + (scrSize?.w ?? 800) + ", " + (scrSize?.h ?? 600) + ");");
            sb.AppendLine("        this.ResumeLayout(false);");
            sb.AppendLine("    }");
            sb.AppendLine();

            // 4) field declarations
            foreach (var c in comps)
                sb.AppendLine("    private BPAddin." + c.typeName + " " + c.name + ";");

            sb.AppendLine("}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void appendHandler(StringBuilder sb, UIComponentInfo c, List<string> screenMethods)
        {
            //MessageBox.Show("screenMethods.Count: " + screenMethods.Count);
            if(EventMap.componentEvents.TryGetValue(c.typeName, out var ev))
            {
                string handlerName = "on" + c.name + ev.eventName;

                // in this way we ignore case mistakes like onbuttonNewClick vs. onButtonNewClick
                // user can enter onButtonNewClick and no fault is done
                if (screenMethods.Contains(handlerName, StringComparer.OrdinalIgnoreCase)) {
                    sb.AppendLine("        this." + c.name + "." + ev.eventName + 
                        " += new " + ev.delegateType + "(this." + handlerName + ");");
                }
            }
        }
    }
}