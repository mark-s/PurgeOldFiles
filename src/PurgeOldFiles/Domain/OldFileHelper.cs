using System.IO;
using System.Linq;

namespace PurgeOldFiles.Domain
{
    public static class OldFileHelper
    {
        public static FolderCollection GetOldFiles(DeleteConfiguration config)
        {
            var allFiles = Directory.GetFileSystemEntries(config.Folder, "*.*", SearchOption.AllDirectories);

            var oldFiles = allFiles.Where(f => config.IsFileOld(f))
                .Select(f => new OldFile(f, config.FileDeleter))
                .ToList();

            var folders = oldFiles.GroupBy(f => f.FilePath)
                .Select(fg => new Folder(fg.Key, fg.ToList(), config.FolderDeleter))
                .ToList<IFolder>();

            var allSubFolders = Directory.EnumerateDirectories(config.Folder, "*", SearchOption.AllDirectories)
                .Select(f => new FolderOnly(f, config.FolderDeleter))
                .ToList<IFolder>();


            return new FolderCollection(folders, allSubFolders);
        }



    }
}
