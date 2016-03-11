using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBackupService.Control
{
    public class ServiceControl
    {
        private const string ServiceName = "SimpleBackupService";
        private ServiceController _controller;

        public ServiceControl ( )
        {
            _controller = new ServiceController(ServiceName);
        }

        
    }
}
