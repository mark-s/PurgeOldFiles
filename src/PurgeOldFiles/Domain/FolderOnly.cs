using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PurgeOldFiles.Providers;

namespace PurgeOldFiles.Domain
{
    public class FolderOnly : IFolder
    {
        private readonly IFolderDeleter _folderDeleter;

        public string Path { get; }
        public List<OldFile> OldFiles { get; } = new List<OldFile>(0);

        public bool FilesDeletedOk { get; } = true;
        public List<string> FileErrors { get; } = new List<string>(0);

        public bool DeletedOk => !Errors.Any();
        public List<string> Errors { get; } = new List<string>();


        public FolderOnly(string path, IFolderDeleter folderDeleter)
        {
            _folderDeleter = folderDeleter;
            Path = path;
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

        public void DeleteOldFilesInFolder() 
            => throw new NotSupportedException("Use the Folder class for folders with files in!");
    }
}