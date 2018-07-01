using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurgeOldFiles.CommandLine;
using PurgeOldFiles.Providers;

namespace PurgeOldFiles.Domain
{
    public class Config
    {
        private readonly Options _options;

        private DateTime? _cutoffDate = null;
        public DateTime CutoffDate
        {
            get
            {
                if (_cutoffDate.HasValue == false)
                    _cutoffDate = DateTime.Now.AddDays(-_options.DaysBefore);

                return _cutoffDate.Value;
            }
        }

        public IFileDeleter FileDeleter
            => _options.Test ? (IFileDeleter)new TestFileDeleter() : new RealFileDeleter();

        public IFolderDeleter FolderDeleter
            => _options.Test ? (IFolderDeleter)new TestFolderDeleter() : new RealFolderDeleter();


        public Config(Options options)
        {
            _options = options;
        }

    }
}
