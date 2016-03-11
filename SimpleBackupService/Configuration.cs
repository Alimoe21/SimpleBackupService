using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService
{
    public class Configuration
    {
        private readonly IBackupConfig _config = null;

        public string BackupScriptName
        {
            get
            {
                if ( _config != null )
                {
                    return _config.BackupScriptName;
                }
                return "backup.bat";
            }
        }

        public bool EnableDebug
        {
            get
            {
                if ( _config != null )
                {
                    return _config.EnableDebug;
                }
                return false;
            }
        }

        public Configuration ( IBackupConfig config )
        {
            _config = config;
        }
    }
}
