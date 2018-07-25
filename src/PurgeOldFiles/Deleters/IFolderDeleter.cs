namespace PurgeOldFiles.Deleters
{
    public interface IFolderDeleter
    {
        void Delete(string path);
    }
}