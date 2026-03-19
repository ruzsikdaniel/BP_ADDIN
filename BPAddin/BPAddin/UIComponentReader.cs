using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BPAddin
{
    public class UIComponentInfo
    {
        public string name;      // "buttonCancel" - value of "className" tagged value
        public string typeName;  // "UIButton" - parent UI Library component name
        public int x, y, w, h;   // position and size in dialog DiagramObject
        public string text;      // element name in UI Diagram - "Cancel"
    }

    public class UIComponentReader
    {

        private EA.Repository repo;
        private Dictionary<string, string> stereotypeMap;
        private const double ea_to_px = 2.0;

        public UIComponentReader(Dictionary<string, string> map)
        {
            this.stereotypeMap = map;
        }
        
        public List<UIComponentInfo> getComponents(EA.Repository repo, EA.Element screenElement)
        {
            this.repo = repo;
            var result = new List<UIComponentInfo>();

            // find UI diagram
            EA.Diagram uiDiagram = findUIDiagram(repo);
            if (uiDiagram == null) {
                MessageBox.Show("UI Diagram not found!");
                return result;
            }
            
            MessageBox.Show("UI Diagram: " + uiDiagram.Name + ", Type: " + uiDiagram.Type);

            // find first dialog element in UI diagram
            EA.Element dialogElement = findDialogForScreen(repo, uiDiagram, screenElement.Name);
            if (dialogElement == null) {
                MessageBox.Show("Dialog element not found!");
                return result;
            }

            MessageBox.Show("Dialog element: " + dialogElement.Name + ", Stereotype: " + dialogElement.Stereotype);

            // show actual child count
            MessageBox.Show("Child elements: " + dialogElement.Elements.Count);
            
            // show number of children with correct tagged values - has className
            foreach (EA.Element child in dialogElement.Elements)
            {
                string tv = getTaggedValue(child, "className");
                MessageBox.Show("Child: " + child.Name + ", Stereotype: " + child.Stereotype + ", className TV: " + (tv ?? "NULL"));
            }

            // find dialog diagram object (shown in diagram)
            EA.DiagramObject dialogDObj = getDiagramObject(uiDiagram, dialogElement.ElementID);

            foreach (EA.Element element in dialogElement.Elements)
            {
                string className = getTaggedValue(element, "className");
                if (string.IsNullOrEmpty(className))
                    continue;

                EA.DiagramObject dObj = getDiagramObject(uiDiagram, element.ElementID);
                if (dObj == null)
                    continue;

                string parentType = getUILibraryParent(repo, element) ?? 
                    (stereotypeMap.TryGetValue(element.Stereotype, out string t) ? t : null);

                int absX = (int)(dObj.left * ea_to_px);
                int absY = (int)(-dObj.top * ea_to_px); // negacia
                int w = (int)((dObj.right - dObj.left) * ea_to_px);
                int h = (int)((dObj.top - dObj.bottom) * ea_to_px);

                // relative position to dialog
                int originX = dialogDObj != null ? (int)(dialogDObj.left * ea_to_px) : 0;
                int originY = dialogDObj != null ? (int)(-dialogDObj.top * ea_to_px) : 0;
            
                result.Add(new UIComponentInfo
                {
                    name = className,
                    typeName = parentType,
                    x = absX - originX,
                    y = absY - originY,
                    w = w,
                    h = h,
                    text = element.Name
                });
            }

            // save dialog size as the first element
            // gets __screen__ so we know it's main
            if (dialogDObj != null)
            {
                int sw = (int)((dialogDObj.right - dialogDObj.left) * ea_to_px);
                int sh = (int)((dialogDObj.top - dialogDObj.bottom) * ea_to_px);
                result.Insert(0, new UIComponentInfo { 
                    name = "__screen__", 
                    w = sw, 
                    h = sh 
                });
                if (dialogDObj == null)
                    MessageBox.Show("dialog dObj not found!");
                else
                    MessageBox.Show("dialog dObj: " + dialogDObj.left + "," + dialogDObj.top + ", " + dialogDObj.right + "," + dialogDObj.bottom);
            }

            return result;
        }

        private EA.Diagram findUIDiagram(EA.Repository repo)
        {
            foreach (EA.Package model in repo.Models)
            {
                EA.Diagram d = findDiagramByName(model, "UI Diagram");
                if (d != null) 
                    return d;
            }
            return null;
        }

        private EA.Diagram findDiagramByName(EA.Package pkg, string name)
        {
            foreach (EA.Diagram d in pkg.Diagrams)
                if (d.Name == name) return d;

            foreach (EA.Package sub in pkg.Packages)
            {
                EA.Diagram d = findDiagramByName(sub, name);
                if (d != null) return d;
            }
            return null;
        }
        private EA.Element findDialogForScreen(EA.Repository repo, EA.Diagram diagram, string screenClassName)
        {
            EA.Element firstScreen = null;

            // find <<win32Dialog>> having screenClass tagged value of screen's classname
            foreach (EA.DiagramObject dobj in diagram.DiagramObjects)
            {
                EA.Element el = repo.GetElementByID(dobj.ElementID);
                if (el == null || el.Stereotype != "win32Dialog") 
                    continue;

                if (firstScreen == null)
                    firstScreen = el;

                string tv = getTaggedValue(el, "screenClass");
                if (tv == screenClassName)
                    return el;
            }

            return firstScreen;
        }

        private EA.DiagramObject getDiagramObject(EA.Diagram diagram, int elementID)
        {
            foreach (EA.DiagramObject dObj in diagram.DiagramObjects)
                if (dObj.ElementID == elementID) 
                    return dObj;
            return null;
        }

        private string getTaggedValue(EA.Element el, string name)
        {
            foreach (EA.TaggedValue tv in el.TaggedValues)
                if (tv.Name == name) 
                    return tv.Value;
            return null;
        }

        private string getUILibraryParent(EA.Repository repo, EA.Element el)
        {
            foreach (EA.Connector connector in el.Connectors)
            {
                if (connector.Type != "Generalization")
                    continue;
                if (connector.ClientID != el.ElementID)
                    continue;
                EA.Element parent = repo.GetElementByID(connector.SupplierID);
                if (parent != null && parent.Name.StartsWith("UI"))
                    return parent.Name;
            }
            return null;
        }
    }
}