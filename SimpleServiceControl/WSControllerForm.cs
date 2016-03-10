using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleBackupServiceControl
{
    public partial class WSControllerForm : Form
    {
        private System.ServiceProcess.ServiceController WSController;

        public WSControllerForm()
        {
            InitializeComponent();
            WSController = new ServiceController();
        }

        private void WSControllerForm_Load(object sender, EventArgs e)
        {
            ServiceController[] AvailableServices = ServiceController.GetServices(".");

            foreach (ServiceController AvailableService in AvailableServices)
            {
                //Check the service name for IIS. 
                if (AvailableService.ServiceName == "SimpleBackupService")
                {
                    WSController.ServiceName = "SimpleBackupService";
                    if ( WSController.Status == ServiceControllerStatus.Stopped )
                    {
                        WSController.Start();
                    }
                    //SetButtonStatus();
                    return;
                }
            }

            MessageBox.Show("The IIS Admin Service is not installed on this Machine", "IIS Admin Service is not available");
            this.Close();
            Application.Exit();
        }
    }
}
