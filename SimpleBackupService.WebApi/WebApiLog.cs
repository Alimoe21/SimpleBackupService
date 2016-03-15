using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService.WebApi
{
    public class WebApiLog
    {
        private readonly IBackupLogging _logging;

        public WebApiLog()
        {
            var directoryCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(directoryCatalog);
            _logging = container.GetExportedValueOrDefault<IBackupLogging>();
        }

        public void Debug ( string message, params object[] args )
        {
            _logging?.Debug(message, args);
        }
    }
}
