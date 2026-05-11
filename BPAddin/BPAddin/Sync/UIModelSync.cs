using EA;
using System.Windows.Forms;

using static BPAddin.util.EABase;

namespace BPAddin
{
    public class UIModelSync
    {
        private EA.Repository repo;
        private StereotypeMap stereotypeMap;
        private EA.Diagram uiDiagram;
        private EA.Diagram appDiagram;
        private EA.Package uiLibPkg;

        public UIModelSync(Repository repository)
        {
            this.repo = repository;
            this.stereotypeMap = new StereotypeMap();
        }

        public void syncAll(string uiPkgName, string appPkgName)
        {
            if (string.IsNullOrEmpty(uiPkgName) || string.IsNullOrEmpty(appPkgName))
            {
                MessageBox.Show("UI Diagram or Application Class Diagram not set.");
                return;
            }

            EA.Package uiPkg = findPackageByName(repo, uiPkgName);
            EA.Package appPkg = findPackageByName(repo, appPkgName);

            if (uiPkg == null)
            {
                MessageBox.Show("UI Diagram package " + uiPkgName + " not found!", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            uiDiagram = findDiagramByName(uiPkg, uiPkgName);


            if (appPkg == null)
            {
                MessageBox.Show("Application Class Diagram package " + appPkgName + " not found!", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            uiLibPkg = findPackageByName(repo, "UI Library");
            if (uiLibPkg == null)
            {
                MessageBox.Show("UI Library package not found!", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Element el in uiPkg.Elements)
            {
                if (el.Stereotype == "win32Dialog")
                {
                    syncScreen(el, appPkg, appPkgName);
                }
            }

            if (appDiagram != null)
            {
                repo.ReloadDiagram(appDiagram.DiagramID);
                repo.RefreshModelView(0);
            }
        }

        private EA.Element findUILibraryElement(string stereotype)
        {
            string uiName = stereotypeMap.map[stereotype];
            return findElementByName(uiLibPkg, uiName);
        }


        // does not create new elements into browser or diagram (attributes, methods, etc. would be also discarded)
        // future works: delete or otherwise modify existing classes with no data loss
        public void syncGUIElement(EA.Element uiEl, EA.Package appClassPkg, EA.Element screenClass, string appPkgName)
        {
            string prefix = StereotypeMap.getClassPrefix(uiEl.Stereotype, stereotypeMap.map);
            if (prefix == null)
                return;

            string className = prefix + uiEl.Name.Trim().Replace(" ", "");

            EA.Element elClass = null;

            // find class in app class package
            foreach(EA.Element element in appClassPkg.Elements)
            {
                if(element.Name == className)
                {
                    elClass = element;
                    break;
                }
            }


            if (elClass == null)
            {
                elClass = (EA.Element)appClassPkg.Elements.AddNew(className, "Class");
                elClass.Stereotype = "UIElement";
                elClass.Update();
                appClassPkg.Elements.Refresh();
            }

            appDiagram = findDiagramByName(appClassPkg, appPkgName);

            if (appDiagram != null && findDobjInDiagram(appDiagram, elClass.ElementID) == null)
            {
                EA.DiagramObject dobj = (EA.DiagramObject)appDiagram.DiagramObjects.AddNew("", "");
                dobj.ElementID = elClass.ElementID;
                dobj.Update();
                appDiagram.DiagramObjects.Refresh();
            }

            EA.Element uiParent = findUILibraryElement(uiEl.Stereotype);
            if (uiParent != null)
            {
                addGeneralizationIfMissing(elClass, uiParent);
            }
            addAssociationIfMissing(screenClass, elClass);
        }

        public void syncScreen(Element screen, Package appClassPkg, string appPkgName)
        {
            string screenClassName = StereotypeMap.getClassPrefix("win32Dialog", stereotypeMap.map) + screen.Name.Trim().Replace(" ", "");    //  <<win32Dialog>>Screen A =>  screenScreenA

            EA.Element screenClass = null;
            
            foreach(EA.Element element in appClassPkg.Elements)
            {
                if(element.Name == screenClassName)
                {
                    screenClass = element;
                    break;
                }
            }


            if (screenClass == null) {
                screenClass = (EA.Element)appClassPkg.Elements.AddNew(screenClassName, "Class");
                screenClass.Stereotype = "UIElement";
                screenClass.Update();
            }

            setTaggedValue(screen, "screenClass", screenClassName);


            EA.Element uiScreenParent = findUILibraryElement("win32Dialog");
            if (uiScreenParent != null)
            {
                addGeneralizationIfMissing(screenClass, uiScreenParent);
            }

            appDiagram = findDiagramByName(appClassPkg, appPkgName);


            if (appDiagram != null && findDobjInDiagram(appDiagram, screenClass.ElementID) == null) {
                EA.DiagramObject dobj = (EA.DiagramObject)appDiagram.DiagramObjects.AddNew("", "");
                dobj.ElementID = screenClass.ElementID;
                dobj.Update();
                appDiagram.DiagramObjects.Refresh();
            }


            // refresh the diagram object as well
            appClassPkg.Elements.Refresh();

            EA.Element freshScreen = repo.GetElementByID(screen.ElementID);
            foreach (EA.Element child in freshScreen.Elements)
            {
                if (!stereotypeMap.map.ContainsKey(child.Stereotype)) continue;
                syncGUIElement(child, appClassPkg, screenClass, appPkgName);
            }
        }
        

        private void addGeneralizationIfMissing(EA.Element child, EA.Element parent)
        {
            foreach (EA.Connector conn in child.Connectors)
                if (conn.Type == "Generalization" && conn.SupplierID == parent.ElementID) 
                    return;

            EA.Connector gen = (EA.Connector)child.Connectors.AddNew("", "Generalization");
            gen.SupplierID = parent.ElementID;
            gen.Update();
            child.Connectors.Refresh();
        }

        private void addAssociationIfMissing(EA.Element owner, EA.Element child)
        {
            foreach (EA.Connector conn in owner.Connectors)
                if (conn.Type == "Association" && conn.SupplierID == child.ElementID) return;

            EA.Connector assoc = (EA.Connector)owner.Connectors.AddNew("", "Association");
            assoc.SupplierID = child.ElementID;
            assoc.Update();
            owner.Connectors.Refresh();
        }

        private EA.DiagramObject findDobjInDiagram(EA.Diagram diagram, int elementID) { 
            
            foreach(EA.DiagramObject dobj in diagram.DiagramObjects)
            {
                if(dobj.ElementID == elementID)
                    return dobj;
            }

            return null;
        }

    }
}
