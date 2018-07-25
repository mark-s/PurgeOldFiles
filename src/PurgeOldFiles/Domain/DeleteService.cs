using System;
using System.Collections.Generic;

namespace PurgeOldFiles.Domain
{
    internal static class DeleteService
    {
        public static IReadOnlyList<string> Delete(DeleteConfiguration config)
        {
            switch (config.FolderDeleteOption)
            {
                case FolderDeleteOption.DeleteEmptiedFolders:
                    return DeleteFilesAndDeleteEmptiedFolders(config);

                case FolderDeleteOption.DeleteAllEmptyFolders:
                    return DeleteFilesAndDeleteAllEmptyFolders(config);

                case FolderDeleteOption.NoDeleteEmptyFolders:
                    return OnlyDeleteFiles(config);

                default:
                    throw new ArgumentOutOfRangeException(nameof(config.FolderDeleteOption));
            }
        }

        private static IReadOnlyList<string> DeleteFilesAndDeleteEmptiedFolders(DeleteConfiguration config)
            => FileSystem.GetOldFiles(config)
                                       .DeleteOldFiles()
                                       .DeleteEmptiedFolders()
                                       .GetErrors();

        private static IReadOnlyList<string> DeleteFilesAndDeleteAllEmptyFolders(DeleteConfiguration config)
            => FileSystem.GetOldFiles(config)
                                       .DeleteOldFiles()
                                       .DeleteAllEmptyFolders()
                                       .GetErrors();

        private static IReadOnlyList<string> OnlyDeleteFiles(DeleteConfiguration config)
            => FileSystem.GetOldFiles(config)
                                       .DeleteOldFiles()
                                       .GetErrors();
    }
}
