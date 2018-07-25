namespace PurgeOldFiles.Deleters
{
    public interface IFileDeleter
    {
        void Delete(string filename);
    }
}