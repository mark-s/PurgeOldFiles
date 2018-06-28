using System;
using System.IO;
using PurgeOldFiles.Providers;

namespace PurgeOldFiles.Domain
{
    internal class OldFile
    {
        private readonly IFileDeleter _fileDeleter;

        public string FullPathAndFileName { get; }

        public string FileName { get;  }

        public string FilePath { get;  }

        public bool DeletedOk { get; private set; }

        public string ErrorMessage { get; private set; }

        public OldFile(string fullPathAndFileName, IFileDeleter fileDeleter)
        {
            _fileDeleter = fileDeleter;
            FullPathAndFileName = fullPathAndFileName;
            FileName = Path.GetFileName(fullPathAndFileName);
            FilePath = Path.GetDirectoryName(fullPathAndFileName);
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