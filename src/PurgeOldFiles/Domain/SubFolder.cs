using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PurgeOldFiles.Deleters;

namespace PurgeOldFiles.Domain
{
    public class SubFolder 
    {
        private readonly IFolderDeleter _folderDeleter;

        public string Path { get; }

        public bool DeletedOk => !Errors.Any();

        public List<string> Errors { get; } = new List<string>();


        public SubFolder(string path, IFolderDeleter folderDeleter)
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

    }
}