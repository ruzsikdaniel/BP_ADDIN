using EA;
using System.Collections.Generic;
using System.Windows.Forms;

using static BPAddin.util.EAMacros;

namespace BPAddin
{
    public class UILibraryInit
    {
        public void initUIElementStereotype(Repository repository)
        {
            object item;
            EA.ObjectType type = repository.GetContextItem(out item);
            if(type != EA.ObjectType.otPackage)
            {
                MessageBox.Show("Please choose a package in the Project Browser.");
                return;
            }

            EA.Package selected = (EA.Package)item;
            if (selected == null)
            {
                MessageBox.Show("No package chosen.", "BPAddin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int count = initClassesPerPackage(selected);
            repository.RefreshModelView(selected.PackageID);

            MessageBox.Show(
                "Done! Stereotype <<UIElement>> was added to " + count + " classes.\n" + "Package: " + selected.Name, "BPAddin – Tag UI Library", MessageBoxButtons.OK, MessageBoxIcon.Information
            );

        }

        private int initClassesPerPackage(EA.Package pkg)
        {
            int count = 0;
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Type == ELTYPE_CLASS)
                {
                    el.Stereotype = STYPE_UIELEMENT;
                    el.Update();
                    count++;
                }
            }

            foreach (EA.Package sub in pkg.Packages)
            {
                count += initClassesPerPackage(sub);
            }

            return count;
        }

        private void collectPackages(EA.Package pkg, List<EA.Package> result)
        {
            result.Add(pkg);
            foreach (EA.Package sub in pkg.Packages)
            {
                collectPackages(sub, result);
            }
        }


    }
}