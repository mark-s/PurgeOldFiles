using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PurgeOldFiles.Domain
{
    public class FolderCollection
    {
        public List<IFolder> FoldersContainingOldFiles { get; }
        public List<IFolder> AllSubFoldersForCleanUp { get; }

        public List<string> Errors { get; } = new List<string>();

        public bool HasErrors => !Errors.Any();

        public FolderCollection(List<IFolder> foldersContainingOldFiles, List<IFolder> allSubFoldersForCleanUp)
        {
            FoldersContainingOldFiles = foldersContainingOldFiles;
            AllSubFoldersForCleanUp = allSubFoldersForCleanUp;
        }


        public FolderCollection DeleteOldFiles()
        {
            foreach (var folder in FoldersContainingOldFiles)
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
            foreach (var folder in FoldersContainingOldFiles.OrderByDescending(f => f.Path.Count(c => c == Path.DirectorySeparatorChar)))
            {
                folder.DeleteIfEmpty();

                if (folder.DeletedOk == false)
                    Errors.AddRange(folder.Errors);
            }

            return this;
        }


        public FolderCollection DeleteAllEmptyFolders()
        {
            // Start at the deepest level
            foreach (var folder in AllSubFoldersForCleanUp.OrderByDescending(f => f.Path.Count(c => c == Path.DirectorySeparatorChar)))
            {
                folder.DeleteIfEmpty();

                if (folder.DeletedOk == false)
                    Errors.AddRange(folder.Errors);
            }

            return this;
        }

    }
}