using System.Diagnostics;

namespace GoodbyeOldFiles.Providers
{
    public class TestFolderDeleter : IFolderDeleter
    {
        public void Delete(string path)
        {
            // Nothing! Don't delete!
            Debug.WriteLine($"Deleted folder: {path}");

        }
    }
}