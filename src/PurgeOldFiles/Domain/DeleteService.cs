using System;
using System.Collections.Generic;

namespace PurgeOldFiles.Domain
{
    public class DeleteService
    {
        public static List<string> Delete(DeleteConfiguration config)
        {
            switch (config.FolderDeleteOption)
            {
                case FolderDeleteOption.DeleteEmptiedFolders:
                    return DeleteAndCleanEmptiedFolders(config);

                case FolderDeleteOption.DeleteAllEmptyFolders:
                    return DeleteAndCleanAllEmptyFolders(config);

                case FolderDeleteOption.NoDeleteEmptyFolders:
                    return DeleteButLeaveEmptyFolders(config);

                default:
                    throw new ArgumentOutOfRangeException(nameof(config.FolderDeleteOption));
            }
        }

        private static List<string> DeleteAndCleanEmptiedFolders(DeleteConfiguration config)
            => OldFileHelper.GetOldFiles(config)
                                .DeleteOldFiles()
                                .DeleteEmptiedFolders()
                                .Errors;

        private static List<string> DeleteAndCleanAllEmptyFolders(DeleteConfiguration config)
            => OldFileHelper.GetOldFiles(config)
                                .DeleteOldFiles()
                                .DeleteAllEmptyFolders()
                                .Errors;

        private static List<string> DeleteButLeaveEmptyFolders(DeleteConfiguration config)
            => OldFileHelper.GetOldFiles(config)
                                .DeleteOldFiles()
                                .Errors;
    }
}
