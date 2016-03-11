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

        public BackupWebApi ( )
        {
            _config = new WebApiConfig();
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
            _server?.OpenAsync ( ).Start ( );
        }

        public void Stop ( )
        {
            _server?.CloseAsync ( ).Wait ( );
        }

        public void Dispose ( )
        {
            _server?.CloseAsync ( ).Wait ( );
        }
    }
}
