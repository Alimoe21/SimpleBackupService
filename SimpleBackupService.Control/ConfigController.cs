using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService.Control
{
    public class ConfigController
    {
        private IBackupConfig _config;

        public bool ConfigAvailable { get { return _config != null; } }

        public ConfigController ( )
        {
            var directoryCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(directoryCatalog);
            _config = container.GetExportedValueOrDefault<IBackupConfig>();
        }
    }
}
