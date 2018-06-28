using System.Collections.Generic;
using System.IO;
using System.Linq;
using PurgeOldFiles.CommandLine;

namespace PurgeOldFiles.Domain
{
    public class DeleteService
    {
        public static List<string> DeleteAndCleanEmptyFolders(Options options)
        {
            return GetOldFiles(options)
                        .DeleteOldFiles()
                        .DeleteEmptiedFolders()
                        .Errors;
        }

        public static List<string> DeleteButLeaveEmptyFolders(Options options)
        {
            return GetOldFiles(options)
                        .DeleteOldFiles()
                        .Errors;
        }

        private static FolderCollection GetOldFiles(Options options)
        {
            var allFiles = Directory.GetFileSystemEntries(options.Folder, "*.*", SearchOption.AllDirectories);

            var oldFiles = allFiles.Where(f => IsFileOld(f, options))
                                                .Select(f => new OldFile(f, options.FileDeleter))
                                                .ToList();

            var folders = oldFiles.GroupBy(f => f.FilePath)
                                               .Select(fg => new Folder(fg.Key, fg.ToList(), options.FolderDeleter))
                                               .ToList();

            return new FolderCollection(folders);
        }

        private static bool IsFileOld(string file, Options options)
        {
            if (options.Created)
                return File.GetCreationTime(file) <= options.CutoffDate;
            else
                return File.GetLastWriteTime(file) <= options.CutoffDate;
        }

    }
}
