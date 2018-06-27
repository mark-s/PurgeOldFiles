using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace GoodbyeOldFiles
{
    public class Options
    {


        [Option('f', "folder", Required = true, HelpText = "Folder to work on")]
        public string Folder { get; set; }

        [Option('d', "days", Required = true, HelpText = "Delete all files older then x days")]
        public int DaysBefore { get; set; }

        [Option(Default = false, HelpText = "Use file Created date")]
        public bool Created { get; set; }

        [Option(Default = true, HelpText = "Use file Modified date")]
        public bool Modified { get; set; }

        [Option(Default = false, HelpText = "Delete Empty Folders if empty after deleting old folders")]
        public bool DeleteEmptyFolders { get; set; }

        [Option(Default = false, HelpText = "Test run only - don't delete anything!")]
        public bool Test { get; set; }


    }
}
