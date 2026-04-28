using System.IO;
using System.Windows.Forms;
using static BPAddin.util.EAMacros;

namespace BPAddin
{
    public class Importer
    {
        private EA.Repository repository;
        private EA.Package uiLibPkg;
        private EA.Diagram uiDiagram;

        public Importer(EA.Repository repository) { 
            this.repository = repository;
        }


        public void importUILibrary(string uiLibPath) {
            
            EA.Project project = repository.GetProjectInterface();
            string pkgGUID = project.GUIDtoXML(uiLibPkg.PackageGUID);

            string[] files = Directory.GetFiles(uiLibPath, "*.cs", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            { 
                string fileName = Path.GetFileName(file);

                if (!fileName.StartsWith("UI"))
                    continue;
                
                project.ImportFile(pkgGUID, "C#", file, "");
            }
            
            setStereotypesForClasses(uiLibPkg, STYPE_UIELEMENT);
        }

        public void initUILibrary(string uiLibPath)
        {
            uiLibPkg = findOrCreatePackage(UILIB_PKGNAME);
            if(uiLibPkg == null)
                return;

            uiDiagram = findOrCreateDiagram(uiLibPkg, UILIB_PKGNAME);

            importUILibrary(uiLibPath);
            populateDiagram(uiLibPkg, uiDiagram);
            repository.RefreshModelView(0);
        }

        private EA.Package findOrCreatePackage(string pkgName) {
            // find the package based on the root
            EA.Package root = (EA.Package)repository.Models.GetAt(0);

            // find package in root
            for(short i = 0; i < root.Packages.Count; i++)
            {
                EA.Package pkg = (EA.Package)root.Packages.GetAt(i);

                if(pkg.Name == pkgName)
                {
                    // delete existing package
                    if (MessageBox.Show(
                        "An existing UI Library has been found.\n" +
                        "Importing now will replace existing UI Library.\n" +
                        "\nContinue?",
                        "BPAddin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) 
                        != DialogResult.Yes)
                        return null;

                    root.Packages.DeleteAt(i, true);
                    root.Packages.Refresh();

                    break;
                }
            }

            // create package to root
            //MessageBox.Show("Creating new package - " + pkgName + " as type " + uiLibPkgType);
            EA.Package pkgNew = (EA.Package)root.Packages.AddNew(UILIB_PKGNAME, UILIB_PKGTYPE);
            pkgNew.Update();

            root.Packages.Refresh();
            return pkgNew;
        }

        private EA.Diagram findOrCreateDiagram(EA.Package pkg, string name)
        {
            // find diagram
            foreach(EA.Diagram diagram in pkg.Diagrams)
            {
                if(diagram.Type == UILIB_DIAGRAMTYPE && diagram.Name == name)
                {
                    return diagram;
                }
            }

            // create diagram
            EA.Diagram diagramNew = (EA.Diagram)pkg.Diagrams.AddNew(UILIB_PKGNAME, UILIB_DIAGRAMTYPE);
            diagramNew.Update();
            pkg.Diagrams.Refresh();
            
            return diagramNew;
        }
    
        private void setStereotypesForClasses(EA.Package pkg, string stereotype)
        {
            int count = 0;
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == ELTYPE_CLASS)
                {
                    el.Stereotype = stereotype;
                    el.Update();
                    count++;
                }
            }

            foreach (EA.Package sub in pkg.Packages)
            {
                setStereotypesForClasses(sub, stereotype);
            }
        }

        private void populateDiagram(EA.Package pkg, EA.Diagram diagram)
        {
            foreach(EA.Element el in pkg.Elements)
            {
                if (el.Type != ELTYPE_CLASS)
                    continue;

                EA.DiagramObject dobj = (EA.DiagramObject)diagram.DiagramObjects.AddNew(el.Name, el.Type);
                dobj.ElementID = el.ElementID;
                dobj.Update();
            }

            diagram.DiagramObjects.Refresh();

            foreach (EA.Package sub in pkg.Packages)
                populateDiagram(sub, diagram);
        }
    }
}
