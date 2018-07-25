using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PurgeOldFiles.Domain
{
    public class FolderCollection
    {
        private readonly List<FolderWithFiles> _foldersContainingOldFiles;
        private readonly List<SubFolder> _foldersForCleanUp;
        private readonly List<string> _errors  = new List<string>();

        public FolderCollection(List<FolderWithFiles> foldersContainingOldFiles, List<SubFolder> foldersForCleanUp)
        {
            _foldersContainingOldFiles = foldersContainingOldFiles;
            _foldersForCleanUp = foldersForCleanUp;
        }
        
        public FolderCollection DeleteOldFiles()
        {
            foreach (var folder in _foldersContainingOldFiles)
            {
                folder.DeleteOldFiles();

                if(folder.FilesDeletedOk == false)
                    _errors.AddRange(folder.FileErrors);
            }

            return this;
        }

        public FolderCollection DeleteEmptiedFolders()
        {
            // Start at the deepest level
            foreach (var folder in _foldersContainingOldFiles.OrderByDescending(f => f.Path.Count(c => c == Path.DirectorySeparatorChar)))
            {
                folder.DeleteIfEmpty();

                if (folder.DeletedOk == false)
                    _errors.AddRange(folder.Errors);
            }

            return this;
        }

        public FolderCollection DeleteAllEmptyFolders()
        {
            // Start at the deepest level
            foreach (var folder in _foldersForCleanUp.OrderByDescending(f => f.Path.Count(c => c == Path.DirectorySeparatorChar)))
            {
                folder.DeleteIfEmpty();

                if (folder.DeletedOk == false)
                    _errors.AddRange(folder.Errors);
            }

            return this;
        }

        public IReadOnlyList<string> GetErrors() => _errors.AsReadOnly();
    }
}