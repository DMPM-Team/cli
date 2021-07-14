using DMPackageManager.CLI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DMPackageManager.CLI {
    /// <summary>
    /// Examines the CWD for whether the project is setup with DMPM, as well as having the setup handler
    /// </summary>
    public static class ProjectExaminer {
        /// <summary>
        /// Is the current directory setup for use with DMPM
        /// </summary>
        /// <returns>A true/false boolean depending on if the CWD is valid for DMPM use</returns>
        public static bool IsSetup() {
            if(ReportFailures().Count == 0) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Detailed reporting of failures.
        /// </summary>
        /// <returns>A list of failures</returns>
        public static List<string> ReportFailures() {
            List<string> issues = new List<string>();
            string cwd = Directory.GetCurrentDirectory();
            // Check if the DMPM directory exists
            if (!Directory.Exists(String.Format("{0}/_dmpm", cwd))) {
                issues.Add("The DMPM directory does not exist. Please run 'dmpm init'");
                // This is fatal so we may as well leave now
                return issues;
            }

            if (!File.Exists(String.Format("{0}/_dmpm/dmpm.json", cwd))) {
                issues.Add("The DMPM data file does not exist. Please run 'dmpm init'");
                // This is fatal so we may as well leave now
                return issues;
            }

            string raw_json = File.ReadAllText(String.Format("{0}/_dmpm/dmpm.json", cwd));

            try {
                DataFile data = JsonConvert.DeserializeObject<DataFile>(raw_json);
            } catch {
                issues.Add("Failed to load the DMPM data file. It may be corrupted. Please run 'dmpm init'");
                return issues; // Again, fatal
            }

            // After writing this, I realise that pretty much every failure means the next step wont work
            // I designed this to be multi-error reporting, but clearly that was a waste of time
            // Oh well
            return issues;
        }
    }
}
