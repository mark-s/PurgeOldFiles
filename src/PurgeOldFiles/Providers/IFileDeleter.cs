namespace PurgeOldFiles.Providers
{
    public interface IFileDeleter
    {
        void Delete(string filename);
    }
}