using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService.WebApi
{
    public class WebApiConfig
    {
        private readonly IBackupConfig _config;

        public WebApiConfig ( )
        {
            var directoryCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(directoryCatalog);
            _config = container.GetExportedValueOrDefault<IBackupConfig>();
        }

        public string WebApiUrl
        {
            get
            {
                if (_config != null)
                {
                    return _config.WebApiUrl;
                }
                return "http://localhost:5000";
            }
        } 
    }
}
