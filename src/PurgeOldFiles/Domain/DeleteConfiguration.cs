using System;
using System.IO;
using PurgeOldFiles.CommandLine;
using PurgeOldFiles.Providers;

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
                    _cutoffDate = DateTime.Now.AddDays(-_options.DaysBefore);

                return _cutoffDate.Value;
            }
        }

        public IFileDeleter FileDeleter
            => _options.Test ? (IFileDeleter)new TestFileDeleter() : new RealFileDeleter();

        public IFolderDeleter FolderDeleter
            => _options.Test ? (IFolderDeleter)new TestFolderDeleter() : new RealFolderDeleter();

        public Predicate<string> IsFileOld { get; private set; }

        public FolderDeleteOption FolderDeleteOption { get; private set; }

        public DeleteConfiguration(Options options)
        {
            _options = options;

            ChoseFileDateToCheck(options);

            GetFolderDeleteSetting(options);
        }

        private void GetFolderDeleteSetting(Options options)
        {
            if (options.DeleteEmptiedFolders)
                FolderDeleteOption = FolderDeleteOption.DeleteEmptiedFolders;

            if (options.DeleteAllEmptyFolders)
                FolderDeleteOption = FolderDeleteOption.DeleteAllEmptyFolders;

            if (options.DontDeleteEmptyFolders)
                FolderDeleteOption = FolderDeleteOption.NoDeleteEmptyFolders;
        }

        private void ChoseFileDateToCheck(Options options)
        {
            if (options.Created)
                IsFileOld = file => File.GetCreationTime(file) <= CutoffDate;

            if (options.Modified)
                IsFileOld = file => File.GetLastWriteTime(file) <= CutoffDate;
        }



    }
}
