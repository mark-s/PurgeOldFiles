using CommandLine;

namespace PurgeOldFiles.CommandLine
{
    public class Options
    {
        [Value(0, MetaName = "folder to clean", HelpText = "Folder to work on.", Required = true)]
        public string Folder { get; set; }

        [Option('d', "days", Required = true, HelpText = "Delete all files older then x days")]
        public int DaysBefore { get; set; }

        [Option('c', "created", Default = false, HelpText = "Use file Created date")]
        public bool Created { get; set; }

        [Option('m', "modified", Default = false, HelpText = "Use file Modified date")]
        public bool Modified { get; set; }
        
        [Option(Default = false, HelpText = "Test run only - don't delete anything!")]
        public bool UseTestMode { get; set; }

        [Option('e', "emptiedFolders", Default = false, HelpText = "Delete folders that became empty because of the file purge")]
        public bool DeleteEmptiedFolders { get; set; }

        [Option('a', "allEmptyFolders", Default = false, HelpText = "Delete any empty folders remaining after the purge")]
        public bool DeleteAllEmptyFolders { get; set; }

        [Option('n', "noDeleteFolders", Default = false, HelpText = "Do not delete any empty folders")]
        public bool DontDeleteEmptyFolders { get; set; }
    }

}
