using DMPackageManager.CLI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DMPackageManager.CLI {
    /// <summary>
    /// Class to hold all the stuff for setting up a directory for DMPM
    /// </summary>
    public static class ProjectInitialiser {
        /// <summary>
        /// Gitignore file for inside dmpm. Ignores the tmp folder
        /// </summary>
        public static string dmpm_gitignore = "tmp";

        /// <summary>
        /// Function to setup the project for use with DMPM
        /// </summary>
        /// <returns>A true/false bool if it succeeded or not</returns>
        public static bool SetupProject(bool force = false) {
            if(ProjectExaminer.IsSetup() && !force) {
                Console.WriteLine("This project is already setup for DMPM.");
                Console.WriteLine("If you would like to reset and reinitialise it, run 'dmpm init -f'");
                return false; // Technically it did fail
            }

            string cwd = Directory.GetCurrentDirectory();
            // Check if the DMPM directory exists
            if (Directory.Exists(String.Format("{0}/_dmpm", cwd))) {
                // Is our setup fucked?
                if(!ProjectExaminer.IsSetup()) {
                    Console.WriteLine("This project is half setup for DMPM, but the setup is invalid.");
                    Console.WriteLine("If you would like to reset and reinitialise it, run 'dmpm init -f'");
                }
            }

            if(force) {
                Console.WriteLine("[WARNING] Forceful init was used. Removing old directory...");
                Directory.Delete(String.Format("{0}/_dmpm", cwd), true);
            }

            // Create our directory
            Directory.CreateDirectory(String.Format("{0}/_dmpm", cwd));
            // Save our data file
            DataFile df = new DataFile();
            string file_output = JsonConvert.SerializeObject(df, Formatting.Indented);
            File.WriteAllText(String.Format("{0}/_dmpm/dmpm.json", cwd), file_output);
            // Save our gitignore
            File.WriteAllText(String.Format("{0}/_dmpm/.gitignore", cwd), dmpm_gitignore);
            // Make a blank includes.dm
            File.Create(String.Format("{0}/_dmpm/includes.dm", cwd));
            // If we got here, it worked
            Console.WriteLine("The project has now been setup for DMPM.");
            Console.WriteLine("You will now want to have your .dme file contain an include for _dmpm/includes.dm");
            Console.WriteLine("Every project/codebase is different, but we recommend including this after your defines, if applicable");
            Console.WriteLine("Example of a code/__DEFINES/dmpm.dm below:");
            Console.WriteLine("");
            Console.WriteLine("// Includes the dmpm file");
            Console.WriteLine("#include \"../../_dmpm/includes.dm\"");
            Console.WriteLine("");
            Console.WriteLine("This will ensure your .dme does not re-order the files around, as well as make sure your DMPM packages can use your defines for configuration.");
            return true;
        }
    }
}
