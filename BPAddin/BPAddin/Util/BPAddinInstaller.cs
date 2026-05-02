using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BPAddin.BPAddinInstaller
{
    
    [RunInstaller(true)]
    public class BPAddinInstaller : System.Configuration.Install.Installer
    {
        // registers BPAddin as EA Add-in in Windows Registry for 32 and 64 bit EA configuration

        private void RunRegasm(bool unregister)
        {
            string dotnetDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
            string regasm = System.IO.Path.Combine(dotnetDir, "regasm.exe");
            string dll = Assembly.GetExecutingAssembly().Location;
            string args = unregister ? $"/unregister \"{dll}\"" : $"\"{dll}\" /codebase";

            var psi = new ProcessStartInfo(regasm, args)
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };
var process = Process.Start(psi);
    process?.WaitForExit();

    if (process?.ExitCode != 0)
        throw new Exception($"regasm failed with exit code {process?.ExitCode}");        }


        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            RegisterEAAddin();
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            UnregisterEAAddin();
        }

        private void RegisterEAAddin()
        {
            string logPath = @"C:\BPAddinInstall.log";

            try
            {
                File.AppendAllText(logPath, "RegisterEAAddin started\n");

                RunRegasm(unregister: false);
                File.AppendAllText(logPath, "RunRegasm done\n");

                string[] keys = new string[]
                {
                    @"Software\Sparx Systems\EAAddins64\BPAddin",
                    @"Software\Sparx Systems\EAAddins\BPAddin",
                    @"Software\WOW6432Node\Sparx Systems\EAAddins\BPAddin"
                };

                foreach (string key in keys)
                {
                    using (var reg = Registry.CurrentUser.CreateSubKey(key))
                    {
                        reg.SetValue("", "BPAddin.AddinClass");
                        File.AppendAllText(logPath, $"Written: {key}\n");
                    }
                }

                File.AppendAllText(logPath, "RegisterEAAddin done\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logPath, $"ERROR: {ex.Message}\n{ex.StackTrace}\n");
                throw;
            }

        }

        private void UnregisterEAAddin()
        {
            RunRegasm(unregister: true);
            string[] keys = new string[]
            {
                @"Software\Sparx Systems\EAAddins64\BPAddin",
                @"Software\Sparx Systems\EAAddins\BPAddin",
                @"Software\WOW6432Node\Sparx Systems\EAAddins\BPAddin"
            };

            foreach (string key in keys)
            {
                Registry.CurrentUser.DeleteSubKey(key, throwOnMissingSubKey: false);
            }
        }
    
    }
}
