using System;
using System.IO;

namespace ShadowCopyTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [LoaderOptimization(LoaderOptimization.MultiDomainHost)]
        [STAThread]
        static void Main(params string[] args)
        {
            /* Enable shadow copying */

            // Get the startup path. Both assemblies (Loader and
            // MyApplication) reside in the same directory:
            string startupPath = Path.GetDirectoryName(
                System.Reflection.Assembly
                .GetExecutingAssembly().Location);

            //string startupPath = @"E:\MyProjects\terms prepaid\terms prepaid\bin\Release";

            string configFile = Path.Combine(
                startupPath,
                "Rep10028_main.exe.config");
            string assembly = Path.Combine(
                startupPath,
                "Rep10028_main.exe");

            // Create the setup for the new domain:
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = "TermsPrepaid";
            setup.ShadowCopyFiles = "true"; // note: it isn't a bool
            setup.ConfigurationFile = configFile;

            // Create the application domain. The evidence of this
            // running assembly is used for the new domain:
            AppDomain domain = AppDomain.CreateDomain(
                "Rep10028_main",
                AppDomain.CurrentDomain.Evidence,
                setup);

            // Start MyApplication by executing the assembly:
            //domain.ExecuteAssembly(assembly);

            string user = (args.Length > 0) ? args[0] : "";
            string pass = (args.Length > 1) ? args[1] : "";

            domain.ExecuteAssembly(assembly, new string[] { startupPath, user, pass });

            //domain.CreateInstanceAndUnwrap("Rep10028", "Rep10028.MainClass");

            // After the MyApplication has finished clean up:
            AppDomain.Unload(domain);
        }
    }
}
