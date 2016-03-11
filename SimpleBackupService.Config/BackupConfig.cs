using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService.Config
{
    [Export(typeof(IBackupConfig))]
    public class BackupConfig : IBackupConfig
    {
        public string BackupScriptName { get; }
        public bool EnableDebug { get; }
        public string WebApiUrl { get; }
    }
}
