using EA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPAddin
{
    public class Importer
    {
        private EA.Repository repository;
        private EA.Package uiLibPkg;
        private EA.Diagram uiDiagram;

        private const string uiLibName = "UI Library";
        private const string uiLibPkgType = "Package";
        private const string uiLibDiaType = "Class"; 
        private const string UI_EL_STEREOTYPE = "UIElement";

        private string uiLibPath;

        public Importer(EA.Repository repository) { 
            this.repository = repository;
        }


        public void importUILibrary() {
            uiLibPath = Properties.Settings.Default.UILibraryDir;
            
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

            
            setStereotypesForClasses(uiLibPkg, UI_EL_STEREOTYPE);
        }

        public void initUILibrary()
        {
            uiLibPkg = findOrCreatePackage(uiLibName);
            uiDiagram = findOrCreateDiagram(uiLibPkg, uiLibName);

            importUILibrary();
            populateDiagram(uiLibPkg, uiDiagram);
        }

        private EA.Package findOrCreatePackage(string pkgName) {
            EA.Package root = (EA.Package)repository.Models.GetAt(0);

            // find package in root


            for(short i = 0; i < root.Packages.Count; i++)
            {
                EA.Package pkg = (EA.Package)root.Packages.GetAt(i);

                if(pkg.Name == pkgName)
                {
                    MessageBox.Show("Package found: " + pkgName);
                    // delete existing package
                    root.Packages.DeleteAt(i, true);
                    root.Packages.Refresh();
                    break;
                }
            }

            // create package to root

            MessageBox.Show("Creating new package - " + pkgName + " as type " + uiLibPkgType);
            EA.Package pkgNew = (EA.Package)root.Packages.AddNew(uiLibName, uiLibPkgType);
            pkgNew.Update();

            root.Packages.Refresh();
            return pkgNew;
        }

        private EA.Diagram findOrCreateDiagram(EA.Package pkg, string name)
        {
            // find diagram
            foreach(EA.Diagram diagram in pkg.Diagrams)
            {
                if(diagram.Type == uiLibDiaType && diagram.Name == name)
                {
                    return diagram;
                }
            }

            // create diagram
            EA.Diagram diagramNew = (EA.Diagram)pkg.Diagrams.AddNew(uiLibName, uiLibDiaType);
            diagramNew.Update();
            pkg.Diagrams.Refresh();
            
            return diagramNew;
        }
    
        private void setStereotypesForClasses(EA.Package pkg, string stereotype)
        {
            int count = 0;
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == "Class")
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
                if (el.Type != "Class")
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
