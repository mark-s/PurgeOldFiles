namespace PurgeOldFiles.Providers
{
    public interface IFolderDeleter
    {
        void Delete(string path);
    }
}