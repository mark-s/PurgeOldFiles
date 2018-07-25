using System;
using System.IO;
using PurgeOldFiles.CommandLine;
using PurgeOldFiles.Deleters;

namespace PurgeOldFiles.Domain
{
    public class DeleteConfiguration
    {
        private readonly Options _options;

        public string Folder => _options.Folder;

        private DateTime? _cutoffDate = null;
        public DateTime CutoffDate
        {
            get
            {
                if (_cutoffDate.HasValue == false)
                    _cutoffDate = DateTime.Today.AddDays(-_options.DaysBefore);

                return _cutoffDate.Value;
            }
        }

        public IFileDeleter FileDeleter
            => _options.UseTestMode ? (IFileDeleter)new TestFileDeleter() : new RealFileDeleter();

        public IFolderDeleter FolderDeleter
            => _options.UseTestMode ? (IFolderDeleter)new TestFolderDeleter() : new RealFolderDeleter();

        public Predicate<string> IsFileOld { get; private set; }

        public FolderDeleteOption FolderDeleteOption { get; private set; }

        public DeleteConfiguration(Options options)
        {
            _options = options;

            SetFileDateChecker(options);

            SetFolderDeleteMode(options);
        }

        private void SetFileDateChecker(Options options)
        {
            if (options.Created)
                IsFileOld = file => File.GetCreationTime(file) <= CutoffDate;

            if (options.Modified)
                IsFileOld = file => File.GetLastWriteTime(file) <= CutoffDate;
        }

        private void SetFolderDeleteMode(Options options)
        {
            if (options.DeleteEmptiedFolders)
                FolderDeleteOption = FolderDeleteOption.DeleteEmptiedFolders;

            if (options.DeleteAllEmptyFolders)
                FolderDeleteOption = FolderDeleteOption.DeleteAllEmptyFolders;

            if (options.DontDeleteEmptyFolders)
                FolderDeleteOption = FolderDeleteOption.NoDeleteEmptyFolders;
        }
    }
}
