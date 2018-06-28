using System.Diagnostics;

namespace GoodbyeOldFiles.Providers
{
    public class TestFileDeleter : IFileDeleter
    {
        public void Delete(string filename)
        {
            // Nothing! Don't delete!
            Debug.WriteLine($"Deleted file: {filename}");
        }
    }
}