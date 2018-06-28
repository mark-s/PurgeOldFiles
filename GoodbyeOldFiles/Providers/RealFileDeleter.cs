using System.IO;

namespace GoodbyeOldFiles.Providers
{
    public class RealFileDeleter : IFileDeleter
    {
        public void Delete(string filename) => File.Delete(filename);
    }
}
