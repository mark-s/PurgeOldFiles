using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PurgeOldFiles.Providers;

namespace PurgeOldFiles.Domain
{
    public class FolderWithFiles
    {
        private readonly IFolderDeleter _folderDeleter;
        private readonly List<OldFile> _oldFilesInThisFolder;

        public string Path { get; }

        public bool FilesDeletedOk => !FileErrors.Any();
        public List<string> FileErrors { get; } = new List<string>();

        public bool DeletedOk => !Errors.Any();
        public List<string> Errors { get; } = new List<string>();


        public FolderWithFiles(string path, List<OldFile> oldFiles, IFolderDeleter folderDeleter)
        {
            Path = path;
            _folderDeleter = folderDeleter;
            _oldFilesInThisFolder = oldFiles;
        }

        public void DeleteOldFiles()
        {
            _oldFilesInThisFolder.ForEach(f => f.Delete());

            if (_oldFilesInThisFolder.Any(f => f.DeletedOk == false))
                FileErrors.AddRange(_oldFilesInThisFolder.Where(f => f.DeletedOk == false).Select(f => f.ErrorMessage));
        }

        public void DeleteIfEmpty()
        {
            try
            {
                if (Directory.GetFileSystemEntries(Path).Any() == false)
                    _folderDeleter.Delete(Path);
            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message);
            }
        }
    }
}