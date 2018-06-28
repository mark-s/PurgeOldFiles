using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PurgeOldFiles.Domain
{
    public class DeleteService
    {
        public static List<string> DeleteAndCleanEmptyFolders(Options options)
        {
            var folderCollection = GetOldFiles(options);
            folderCollection.DeleteOldFiles();
            folderCollection.DeleteEmptiedFolders();

            return folderCollection.Errors;
        }

        public static List<string> DeleteButLeaveEmptyFolders(Options options)
        {
            var folderCollection = GetOldFiles(options);
            folderCollection.DeleteOldFiles();

            return folderCollection.Errors;
        }

        private static FolderCollection GetOldFiles(Options options)
        {
            var allFiles = Directory.GetFileSystemEntries(options.Folder, "*.*", SearchOption.AllDirectories);

            var oldFiles = allFiles.Where(f => IsFileOld(options, f))
                                                .Select(f => new OldFile(f, options.FileDeleter))
                                                .ToList();

            var folders = oldFiles.GroupBy(f => f.FilePath)
                                               .Select(fg => new Folder(fg.Key, fg.ToList(), options.FolderDeleter))
                                               .ToList();

            return new FolderCollection(folders);
        }

        private static bool IsFileOld(Options options, string file)
        {
            var fileDate = options.Created ? Directory.GetCreationTime(file) : Directory.GetLastWriteTime(file);
            return fileDate <= options.CutoffDate;
        }

    }
}
