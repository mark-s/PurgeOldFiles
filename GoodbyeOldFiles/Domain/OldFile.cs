using System;
using GoodbyeOldFiles.Providers;

namespace GoodbyeOldFiles.Domain
{
    internal class OldFile
    {
        private readonly IFileDeleter _fileDeleter;
        public string FullPathAndFileName { get; }

        public bool DeletedOk { get; private set; }

        public string ErrorMessage { get; private set; }

        public OldFile(string fullPathAndFileName, IFileDeleter fileDeleter)
        {
            FullPathAndFileName = fullPathAndFileName;
            _fileDeleter = fileDeleter;
        }

        public void Delete()
        {
            try
            {
                _fileDeleter.Delete(FullPathAndFileName);
                DeletedOk = true;
            }
            catch (Exception ex)
            {
                DeletedOk = false;
                ErrorMessage = ex.Message;
            }
        }
    }
}