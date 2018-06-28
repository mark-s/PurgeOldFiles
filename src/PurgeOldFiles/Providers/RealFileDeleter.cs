using System.IO;

namespace PurgeOldFiles.Providers
{
    public class RealFileDeleter : IFileDeleter
    {
        public void Delete(string filename) => File.Delete(filename);
    }
}
