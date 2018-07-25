using System;
using System.IO;

namespace PurgeOldFiles.Deleters
{
    public class TestFileDeleter : IFileDeleter
    {
        public void Delete(string filename)
        {
            var fileInfo = new FileInfo(filename);
            Console.WriteLine($"[TEST][c:{fileInfo.CreationTime.ToShortDateString()}][m:{fileInfo.LastWriteTime.ToShortDateString()}] File: {filename}");
        }
    }
}