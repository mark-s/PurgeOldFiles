using System.Collections.Generic;
using System.Linq;

namespace GoodbyeOldFiles.Domain
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


        public void DeleteOldFiles()
        {
            foreach (var folder in Folders)
            {
                folder.DeleteOldFilesInFolder();
                if(folder.FilesDeletedOk == false)
                    Errors.AddRange(folder.FileErrors);
            }
        }

        public void DeleteEmptiedFolders()
        {
            foreach (var folder in Folders)
            {
                folder.DeleteIfEmpty();
                if (folder.DeletedOk == false)
                    Errors.AddRange(folder.Errors);
            }
        }


    }
}