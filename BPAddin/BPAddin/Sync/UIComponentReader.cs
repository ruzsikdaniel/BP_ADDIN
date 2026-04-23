using System.Collections.Generic;
using System.Windows.Forms;
using BPAddin.Model;
using static BPAddin.util.EABase;

namespace BPAddin
{
    public class UIComponentReader
    {
        private StereotypeMap stereotypeMap;
        private const double ea_to_px = 2.0;
        private EA.Repository repo;

        public UIComponentReader(EA.Repository repository)
        {
            this.repo = repository;
            stereotypeMap = new StereotypeMap();
        }

        public List<UIComponentInfo> getComponents(EA.Repository repo, EA.Element screenElement)
        {
            this.repo = repo;
            var result = new List<UIComponentInfo>();

            EA.Diagram uiDiagram = findUIDiagram(repo);
            if (uiDiagram == null)
            {
                MessageBox.Show("UI Diagram not found!");
                return result;
            }

            EA.Element dialogElement = findDialogForScreen(repo, uiDiagram, screenElement.Name);
            if (dialogElement == null)
            {
                MessageBox.Show("Dialog element not found!");
                return result;
            }

            EA.DiagramObject dialogDObj = findDiagramObject(uiDiagram, dialogElement.ElementID);

            EA.Element freshDialog = repo.GetElementByID(dialogElement.ElementID);
            foreach (EA.Element element in freshDialog.Elements)
            {
                string prefix = StereotypeMap.getClassPrefix(element.Stereotype, stereotypeMap.map);
                if (prefix == null) continue;
                string className = prefix + element.Name.Trim().Replace(" ", "");

                EA.DiagramObject dObj = findDiagramObject(uiDiagram, element.ElementID);
                if (dObj == null) continue;

                string parentType = findUILibraryParent(repo, element) ??
                    (stereotypeMap.map.TryGetValue(element.Stereotype, out string t) ? t : null);

                int absX = (int)(dObj.left * ea_to_px);
                int absY = (int)(-dObj.top * ea_to_px);
                int w = (int)((dObj.right - dObj.left) * ea_to_px);
                int h = (int)((dObj.top - dObj.bottom) * ea_to_px);

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

            if (dialogDObj != null)
            {
                int sw = (int)((dialogDObj.right - dialogDObj.left) * ea_to_px);
                int sh = (int)((dialogDObj.top - dialogDObj.bottom) * ea_to_px);
                result.Insert(0, new UIComponentInfo { name = "__screen__", w = sw, h = sh });
            }

            return result;
        }

        private EA.Diagram findUIDiagram(EA.Repository repo)
        {
            foreach (EA.Package model in repo.Models)
            {
                EA.Diagram d = findDiagramByName(model, "UI Diagram");
                if (d != null) return d;
            }
            return null;
        }

        

        private EA.Element findDialogForScreen(EA.Repository repo, EA.Diagram diagram, string screenClassName)
        {
            EA.Element firstScreen = null;

            foreach (EA.DiagramObject dobj in diagram.DiagramObjects)
            {
                EA.Element el = repo.GetElementByID(dobj.ElementID);
                if (el == null || el.Stereotype != "win32Dialog") continue;

                if (firstScreen == null)
                    firstScreen = el;

                string tv = findTaggedValue(el, "screenClass");
                if (tv == screenClassName)
                    return el;
            }

            return firstScreen;
        }

        private EA.DiagramObject findDiagramObject(EA.Diagram diagram, int elementID)
        {
            foreach (EA.DiagramObject dObj in diagram.DiagramObjects)
                if (dObj.ElementID == elementID) 
                    return dObj;
            return null;
        }

        private string findTaggedValue(EA.Element el, string name)
        {
            foreach (EA.TaggedValue tv in el.TaggedValues)
                if (tv.Name == name) return tv.Value;
            return null;
        }

        private string findUILibraryParent(EA.Repository repo, EA.Element el)
        {
            // element has a UI Library parent when:
            // - has Generalization connector
            // - the connector's source is the element
            // - the name of the connector's target starts with "UI" - UIScreen, UIButton, ...

            foreach (EA.Connector connector in el.Connectors)
            {
                if (connector.Type != "Generalization") continue;
                if (connector.ClientID != el.ElementID) continue;

                EA.Element parent = repo.GetElementByID(connector.SupplierID);

                if (parent != null && parent.Name.StartsWith("UI"))
                    return parent.Name;
            }
            return null;
        }
    }
}