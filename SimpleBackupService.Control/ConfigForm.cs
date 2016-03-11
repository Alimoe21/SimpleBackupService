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
    public partial class ConfigForm : Form
    {
        private ConfigController _configController;
        
        public ConfigForm(ConfigController _configController)
        {
            InitializeComponent();
            this._configController = _configController;
        }
    }
}
