using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

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
            }
            catch { }
        }
    }
}
