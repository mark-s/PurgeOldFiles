using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GoodbyeOldFiles.Providers;

namespace GoodbyeOldFiles.Domain
{
    internal class Folder
    {
        private readonly IFolderDeleter _folderDeleter;
        public string Path { get; }
        public List<OldFile> OldFiles { get; }

        public bool FilesDeletedOk => !FileErrors.Any();
        public List<string> FileErrors { get; } = new List<string>();

        public bool DeletedOk => !Errors.Any();
        public List<string> Errors { get; } = new List<string>();


        public Folder(string path, List<OldFile> oldFiles, IFolderDeleter folderDeleter)
        {
            _folderDeleter = folderDeleter;
            Path = path;
            OldFiles = oldFiles;
        }

        internal void DeleteOldFilesInFolder()
        {
            OldFiles.ForEach(f => f.Delete());

            if (OldFiles.Any(f => f.DeletedOk == false))
                FileErrors.AddRange(OldFiles.Where(f => f.DeletedOk == false).Select(f => f.ErrorMessage));
        }

        internal void DeleteIfEmpty()
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