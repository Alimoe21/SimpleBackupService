using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService.Logging
{
    [Export(typeof(IBackupLogging))]
    public class BackupLogging : IBackupLogging
    {
        public void LogDebug ( string message, params object [ ] d )
        {
            throw new NotImplementedException ( );
        }
    }
}
