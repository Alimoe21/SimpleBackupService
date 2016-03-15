using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBackupService.Contracts
{
    public interface IBackupLogging
    {
        void Debug ( string message, params object [ ] args );
        void Info ( string message, params object [ ] args );
    }
}
