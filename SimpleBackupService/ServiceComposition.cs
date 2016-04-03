using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;
using System.Diagnostics;

namespace SimpleBackupService
{
    public class ServiceComposition
    {
        public static void Compose ( SimpleBackupService service )
        {
            try
            {
                var directoryCatalog = new DirectoryCatalog(".");
                var container = new CompositionContainer(directoryCatalog);
                service.Config = new Configuration(container.GetExportedValueOrDefault<IBackupConfig>());
                service.WebApi = new WebApi(container.GetExportedValueOrDefault<IBackupWebApi>());
                service.Log = new Logging(container.GetExportedValueOrDefault<IBackupLogging>());

                var message = new StringBuilder();
                message.AppendLine("Composition finished!");
                message.AppendLine("Status:");
                message.AppendFormat("\tConfig: {0}\n", service.Config != null ? "found" : "not found");
                message.AppendFormat("\tWebApi: {0}\n", service.WebApi != null ? "found" : "not found");
                message.AppendFormat("\tLog: {0}\n", service.Log != null ? "found" : "not found");

                EventLog.WriteEntry("SimpleBackupService", message.ToString());
            }
            catch { }
        }
    }
}
