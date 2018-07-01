using System.Collections.Generic;

namespace PurgeOldFiles.Domain
{
    public interface IFolder
    {
        bool DeletedOk { get; }
        List<string> Errors { get; }
        List<string> FileErrors { get; }
        bool FilesDeletedOk { get; }
        List<OldFile> OldFiles { get; }
        string Path { get; }
        void DeleteOldFilesInFolder();
        void DeleteIfEmpty();
    }
}