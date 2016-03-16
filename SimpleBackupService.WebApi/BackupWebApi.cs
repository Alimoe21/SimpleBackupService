using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using SimpleBackupService.Contracts;

namespace SimpleBackupService.WebApi
{
    [Export(typeof(IBackupWebApi))]
    public class BackupWebApi : IBackupWebApi
    {
        private readonly HttpSelfHostServer _server;
        private readonly WebApiConfig _config;
        private readonly WebApiLog _log;
        private IBackupService _service;

        public BackupWebApi ( )
        {
            _config = new WebApiConfig();
            _log = new WebApiLog();
            var configuration = new HttpSelfHostConfiguration (_config.WebApiUrl);

            configuration.Routes.MapHttpRoute (
                name: "API",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            _server = new HttpSelfHostServer ( configuration );
        }

        public void Start ( )
        {
            _log.Debug ( "Starting WebApi at URL '{0}'.", _config.WebApiUrl );
            _server?.OpenAsync ( ).Start ( );
        }

        public void Stop ( )
        {
            _server?.CloseAsync ( ).Wait ( );
            _log.Debug("Stoping WebApi.");
        }

        public void Dispose ( )
        {
            _service = null;
            _server?.CloseAsync ( ).Wait ( );
        }

        public void InitWebApi ( IBackupService service )
        {
            _service = service;
        }
    }
}
