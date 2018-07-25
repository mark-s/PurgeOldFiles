using System;
using System.IO;
using PurgeOldFiles.Deleters;

namespace PurgeOldFiles.Domain
{
    public class OldFile
    {
        private readonly IFileDeleter _fileDeleter;

        public string FullPathAndFileName { get; }

        public string FileName { get;  }

        public string FilePath { get;  }

        public bool DeletedOk { get; private set; }

        public string ErrorMessage { get; private set; }

        internal OldFile(string fullPathAndFileName, IFileDeleter fileDeleter)
        {
            _fileDeleter = fileDeleter;
            FullPathAndFileName = fullPathAndFileName;
            FileName = Path.GetFileName(fullPathAndFileName);
            FilePath = Path.GetDirectoryName(fullPathAndFileName);
        }

        internal void Delete()
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