using System;
using System.Collections.Generic;
using System.Text;

namespace DMPackageManager.CLI.Models {
    public class DataFile {
        public string comment { get; set;  } = "This file is automatically created and managed by DMPM. Manual modification is not recommended.";
        public Dictionary<string, string> package_versions { get; set; } = new Dictionary<string, string>();
    }
}
