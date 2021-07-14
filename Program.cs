using System;
using System.Diagnostics;
using System.Reflection;

namespace DMPackageManager.CLI {
    class Program {
        static void Main(string[] args) {
            // TODO: Assign somewhere as a cache
            string dmpm_version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            Console.WriteLine(String.Format("[===] DMPM CLI - v{0} [===]", dmpm_version));
            Console.WriteLine("");
            if (args.Length == 0) {
                Console.WriteLine("Run 'dmpm help' for a list of commands");
                Environment.Exit(1); // This technically wasnt a valid run
            }
            // If we are here we have atleast 1 arg
            switch(args[0]) {
                // Help menu
                case "help":
                    Console.WriteLine("Commands:");
                    Console.WriteLine("dmpm init - Initialise a project folder for use with DMPM");
                    Console.WriteLine("dmpm add <package> - Add a package to the project");
                    Console.WriteLine("dmpm remove <package> - Remove a package from the project");
                    Console.WriteLine("dmpm check [package] - Check one (or all) package for updates");
                    Console.WriteLine("dmpm update [package] - Update one (or all) package to its latest version");
                    Console.WriteLine("dmpm install - Installs all packages required by this project");
                    Console.WriteLine("");
                    Console.WriteLine("Key: <required_argument> [optional_argument]");       
                    break;
                case "init":
                    if(args.Length >= 2) {
                        if(args[1] == "-f") {
                            // They forced it
                            ProjectInitialiser.SetupProject(true);
                        } else {
                            // Do it normally
                            ProjectInitialiser.SetupProject();
                        }
                    } else {
                        // No extra args. Do it normally
                        ProjectInitialiser.SetupProject();
                    }
                    break;
                    
                default:
                    Console.WriteLine("Unknown command. Run 'dmpm help' for a list of commands");
                    break;
            }
        }
    }
}
