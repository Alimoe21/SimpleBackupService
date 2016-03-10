using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBackupService
{
    [RunInstaller(true)]
    public class SimpleBackupServiceInstaller : Installer
    {
        public SimpleBackupServiceInstaller ( )
        {
            var process = new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem };
            var serviceAdmin = new ServiceInstaller
            {
                StartType = ServiceStartMode.Automatic,
                ServiceName = "SimpleBackupService",
                DisplayName = "SimpleBackupService"

            };
            Installers.Add(process);
            Installers.Add(serviceAdmin);
        }
    }
}
