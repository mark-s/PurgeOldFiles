using System;

namespace PurgeOldFiles.Providers
{
    public class TestFolderDeleter : IFolderDeleter
    {
        public void Delete(string path) => Console.WriteLine($"[TEST] Deleted folder: {path}");
    }
}