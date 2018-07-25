using System.IO;
using System.Linq;

namespace PurgeOldFiles.Domain
{
    public static class FileSystem
    {

        public static FolderCollection GetOldFiles(DeleteConfiguration config)
        {
            var allFiles = Directory.GetFileSystemEntries(config.Folder, "*.*", SearchOption.AllDirectories);

            var oldFiles = allFiles.Where(f => config.IsFileOld(f))
                .Select(f => new OldFile(f, config.FileDeleter))
                .ToList();

            var foldersWithOldFiles = oldFiles.GroupBy(f => f.FilePath)
                .Select(fg => new FolderWithFiles(fg.Key, fg.ToList(), config.FolderDeleter))
                .ToList();

            var allSubFolders = Directory.EnumerateDirectories(config.Folder, "*", SearchOption.AllDirectories)
                .Select(f => new SubFolder(f, config.FolderDeleter))
                .ToList();


            return new FolderCollection(foldersWithOldFiles, allSubFolders);
        }



    }
}
