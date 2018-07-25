using System.IO;

namespace PurgeOldFiles.Deleters
{
    public class RealFileDeleter : IFileDeleter
    {
        public void Delete(string filename) => File.Delete(filename);
    }
}
