using System;
using System.IO;

namespace BPAddin.Util
{
    public static class AddinPaths
    {
        // C:\Users\<user>\AppData\Roaming\BPAddin\UILibrary
        public static string UILibraryPath => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "BPAddin", "UILibrary");

        // C:\Users\<user>\Documents\BPAddin\Generated
        public static string DefaultGeneratedDir => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "BPAddin", "Generated Classes");

        // C:\Users\<user>\Documents\BPAddin\Project
        public static string DefaultProjectDir => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "BPAddin", "Project");
    }
}
