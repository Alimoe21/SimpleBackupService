using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBackupService.Contracts
{
    public interface IBackupLogging
    {
        void LogDebug ( string message, params object [ ] d );
    }
}
