using EA;
using System.Collections.Generic;

namespace BPAddin.util
{
    public static class EABase
    {
        // all commonly used/useable Addin methods are located here
        // using this static class enables seamless function calls

        public static EA.Package findPackageByName(EA.Repository repo, string name)
        {
            foreach (EA.Package model in repo.Models)
            {
                if (model.Name == name) return model;
                EA.Package found = findPackageByName(model, name);
                if (found != null) return found;
            }
            return null;
        }

        public static EA.Package findPackageByName(EA.Package pkg, string name)
        {
            foreach (EA.Package sub in pkg.Packages)
            {
                if (sub.Name == name) return sub;
                EA.Package found = findPackageByName(sub, name);
                if (found != null) return found;
            }
            return null;
        }

        public static EA.Element findElementByName(EA.Package pkg, string name)
        {
            foreach (EA.Element el in pkg.Elements)
            {
                if (el.Name == name)
                    return el;
            }

            foreach (EA.Package sub in pkg.Packages)
            {
                EA.Element el = findElementByName(sub, name);
                if (el != null)
                    return el;
            }
            return null;
        }

        public static EA.Diagram findDiagramByName(EA.Package pkg, string name)
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

        public static void setTaggedValue(EA.Element el, string name, string value)
        {
            foreach (EA.TaggedValue tv in el.TaggedValues)
            {
                if (tv.Name == name)
                {
                    tv.Value = value;
                    tv.Update();
                    return;
                }
            }

            EA.TaggedValue new_tv = (EA.TaggedValue)el.TaggedValues.AddNew(name, "");
            new_tv.Value = value;   

            new_tv.Update();
            el.TaggedValues.Refresh();
        }


        public static List<EA.Package> findAllPackages(Repository repo)
        {
            List<EA.Package> result = new List<EA.Package>();

            foreach (EA.Package model in repo.Models)
                collectPackage(model, result);
            return result;
        }

        private static void collectPackage(EA.Package pkg, List<EA.Package> result)
        {
            result.Add(pkg);
            foreach (EA.Package sub in pkg.Packages)
            {
                collectPackage(sub, result);
            }
        }
    }
}
