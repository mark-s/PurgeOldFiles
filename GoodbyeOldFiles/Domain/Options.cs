using System;
using CommandLine;
using GoodbyeOldFiles.Providers;

namespace GoodbyeOldFiles.Domain
{
    public class Options
    {

        [Value(0, MetaName = "folder to clean",
            HelpText = "Folder to work on.",
            Required = true)]
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

        private DateTime? _cutoffDate = null;
        public DateTime CutoffDate
        {
            get
            {
                if (_cutoffDate.HasValue == false)
                    _cutoffDate = DateTime.Now.AddDays(-DaysBefore);

                return _cutoffDate.Value;
            }
        }


        public IFileDeleter FileDeleter 
            => Test ? (IFileDeleter) new TestFileDeleter() : new RealFileDeleter();

        public IFolderDeleter FolderDeleter
            => Test ? (IFolderDeleter)new TestFolderDeleter() : new RealFolderDeleter();
    }
}
