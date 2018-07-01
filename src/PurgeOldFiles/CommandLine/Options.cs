using CommandLine;

namespace PurgeOldFiles.CommandLine
{
    public class Options
    {
        [Value(0, MetaName = "folder to clean", HelpText = "Folder to work on.", Required = true)]
        public string Folder { get; }

        [Option('d', "days", Required = true, HelpText = "Delete all files older then x days")]
        public int DaysBefore { get; }

        [Option('c', "created", Default = false, HelpText = "Use file Created date")]
        public bool Created { get; }

        [Option('m', "modified", Default = true, HelpText = "Use file Modified date")]
        public bool Modified { get; }

        [Option("deleteEmptied", Default = false, HelpText = "Delete folders that became empty because of the file purge")]
        public bool DeleteEmptiedFolders { get; }

        [Option("deleteAllEmpty", Default = false, HelpText = "Delete any empty folders remaining after the purge")]
        public bool DeleteAllEmptyFolders { get; }

        [Option(Default = false, HelpText = "Test run only - don't delete anything!")]
        public bool Test { get; }

        public Options(string folder, int days, bool created, bool modified, bool deleteEmptied, bool deleteAllEmpty, bool test)
        {
            Folder = folder;
            DaysBefore = days;
            Created = created;
            Modified = modified;
            DeleteEmptiedFolders = deleteEmptied;
            DeleteAllEmptyFolders = deleteAllEmpty;
            Test = test;
        }
    }

}
