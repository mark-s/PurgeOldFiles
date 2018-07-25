using System.IO;

namespace PurgeOldFiles.Deleters
{
    public class RealFolderDeleter : IFolderDeleter
    {
        public void Delete(string path) => Directory.Delete(path);
    }
}