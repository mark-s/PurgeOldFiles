using System.Collections.Generic;
using System.Linq;

namespace PurgeOldFiles.Domain
{
    internal class FolderCollection
    {
        public List<Folder> Folders { get; }

        public List<string> Errors { get; } = new List<string>();

        public bool HasErrors => !Errors.Any();

        public FolderCollection(List<Folder> folders)
        {
            Folders = folders;
        }


        public FolderCollection DeleteOldFiles()
        {
            foreach (var folder in Folders)
            {
                folder.DeleteOldFilesInFolder();

                if(folder.FilesDeletedOk == false)
                    Errors.AddRange(folder.FileErrors);
            }

            return this;
        }

        public FolderCollection DeleteEmptiedFolders()
        {
            // Start at the deepest level
            foreach (var folder in Folders.OrderByDescending(f => f.Path.Length))
            {
                folder.DeleteIfEmpty();

                if (folder.DeletedOk == false)
                    Errors.AddRange(folder.Errors);
            }

            return this;
        }


    }
}