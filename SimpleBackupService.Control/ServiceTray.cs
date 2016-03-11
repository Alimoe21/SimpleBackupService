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

namespace SimpleBackupService.Control
{
    public partial class ServiceTray : Form
    {
        private const string ServiceName = "SimpleBackupService";
        private ServiceController _controller;
        private ConfigController _configController;

        public ServiceTray()
        {
            InitializeComponent();
            //this.Hide();
            _controller = new ServiceController(ServiceName);
            _configController = new ConfigController();

            InitializeNotifyIcon();
        }

        private void InitializeNotifyIcon()
        {
            WSNotifyIcon.Text = "Right Click to Configure";
            WSNotifyIcon.Visible = true;
            
            var menuItems = new List<MenuItem > ();
            
            menuItems.Add(new MenuItem("Start Service", StartService));
            menuItems [ 0 ].Enabled = _controller.Status == ServiceControllerStatus.Stopped;
            menuItems.Add(new MenuItem("Stop Service", StopService));
            menuItems[1].Enabled = _controller.Status == ServiceControllerStatus.Running;
            if ( _configController.ConfigAvailable )
            {
                menuItems.Add(new MenuItem("Edit Configuration", ShowConfigForm));
            }

            menuItems.Add(new MenuItem("Exit", ExitControlForm));

            //add the menu items to the context menu of the NotifyIcon 
            ContextMenu notifyIconMenu = new ContextMenu(menuItems.ToArray());
            WSNotifyIcon.ContextMenu = notifyIconMenu;
        }

        private void ShowConfigForm ( object sender, EventArgs e )
        {
            var form = new ConfigForm(_configController);
            form.Show();
        }

        private void StopService ( object sender, EventArgs e )
        {
            _controller.Stop();
            _controller.WaitForStatus(ServiceControllerStatus.Stopped);
        }

        private void StartService ( object sender, EventArgs e )
        {
            _controller.Start();
            _controller.WaitForStatus(ServiceControllerStatus.Running);
        }

        private void ExitControlForm ( object sender, EventArgs e )
        {
            WSNotifyIcon.Visible = false;

            Close();
        }
    }
}
