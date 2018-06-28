using System;

namespace PurgeOldFiles.Providers
{
    public class TestFileDeleter : IFileDeleter
    {
        public void Delete(string filename) => Console.WriteLine($"[TEST] Deleted file: {filename}");
    }
}