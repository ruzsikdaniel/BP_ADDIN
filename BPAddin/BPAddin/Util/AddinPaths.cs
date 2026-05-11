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

        // C:\Users\<user>\Documents\BPAddin\GeneratedClasses
        public static string DefaultGeneratedDir => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "BPAddin", "GeneratedClasses");

        // C:\Users\<user>\Documents\BPAddin\Project
        public static string DefaultProjectDir => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "BPAddin", "Project");
    }
}
