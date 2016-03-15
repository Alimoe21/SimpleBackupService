using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SimpleBackupService.Contracts;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "SimpleBackupService.Logging.dll.config", Watch = true)]

namespace SimpleBackupService.Logging
{
    [Export(typeof(IBackupLogging))]
    public class BackupLogging : IBackupLogging
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public void Debug ( string message, params object [ ] args )
        {
            Log.DebugFormat(message, args);
        }

        public void Info ( string message, params object [ ] args )
        {
            Log.InfoFormat(message, args);
        }
    }
}
