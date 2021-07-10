using System;
using System.Diagnostics;
using System.Reflection;

namespace DMPackageManager.CLI {
    class Program {
        static void Main(string[] args) {
            // TODO: Assign somewhere as a cache
            string dmpm_version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            Console.WriteLine(String.Format("DMPM CLI - v{0}", dmpm_version));
            if(args.Length == 0) {
                Console.WriteLine("Run 'dmpm help' for a list of commands");
            }
        }
    }
}
