using System.IO;

namespace GoodbyeOldFiles.Providers
{
    public class RealFolderDeleter : IFolderDeleter
    {
        public void Delete(string path) => Directory.Delete(path);
    }
}