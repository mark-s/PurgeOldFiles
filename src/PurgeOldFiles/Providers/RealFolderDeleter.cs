using System.IO;

namespace PurgeOldFiles.Providers
{
    public class RealFolderDeleter : IFolderDeleter
    {
        public void Delete(string path) => Directory.Delete(path);
    }
}