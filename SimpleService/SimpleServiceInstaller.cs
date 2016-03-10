using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService
{
    [RunInstaller(true)]
    public class SimpleServiceInstaller : Installer
    {
        public SimpleServiceInstaller ( )
        {
            var process = new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem };
            var serviceAdmin = new ServiceInstaller
            {
                StartType = ServiceStartMode.Automatic,
                ServiceName = "SimpleService",
                DisplayName = "SimpleService"
                
            };
            Installers.Add(process);
            Installers.Add(serviceAdmin);
        }
    }
}
