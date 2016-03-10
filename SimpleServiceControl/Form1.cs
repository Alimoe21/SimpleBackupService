using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleServiceControl
{
    public partial class Form1 : Form
    {
        //private Icon mDirIcon = new Icon(typeof(SystemTray).Assembly.GetManifestResourceStream("SystemTrayExample.FLGUSA02.ICO"));
        //private System.Windows.Forms.NotifyIcon WSNotifyIcon;

        public Form1()
        {
            InitializeComponent();
            this.Hide();
            InitializeNotifyIcon();
        }

        private void InitializeNotifyIcon()
        {
            //setup the Icon 
            //WSNotifyIcon = new NotifyIcon();
            //WSNotifyIcon.Icon = mDirIcon;
            WSNotifyIcon.Text = "Right Click to Configure";
            WSNotifyIcon.Visible = true;

            //Create the MenuItem objects and add them to 
            //the context menu of the NotifyIcon. 
            MenuItem[] mnuItems = new MenuItem[4];

            //create the menu items array 
            mnuItems[0] = new MenuItem("Show Control Form...", new EventHandler(this.ShowControlForm));
            mnuItems[0].DefaultItem = true;
            mnuItems[1] = new MenuItem("-");
            mnuItems[2] = new MenuItem("Exit", new EventHandler(this.ExitControlForm));
            mnuItems[3] = new MenuItem("Test", new EventHandler(this.Menu3Clicked));
            mnuItems [ 3 ].Checked = true;

            //add the menu items to the context menu of the NotifyIcon 
            ContextMenu notifyIconMenu = new ContextMenu(mnuItems);
            WSNotifyIcon.ContextMenu = notifyIconMenu;
        }

        private void Menu3Clicked ( object sender, EventArgs e )
        {
            WSNotifyIcon.ContextMenu.MenuItems [ 3 ].Checked = !WSNotifyIcon.ContextMenu.MenuItems [ 3 ].Checked;
        }

        private void ExitControlForm ( object sender, EventArgs e )
        {
            WSNotifyIcon.Visible = false;

            this.Close();
        }

        private void ShowControlForm ( object sender, EventArgs e )
        {
            WSControllerForm controlForm = new WSControllerForm();
            controlForm.Show();
        }
    }
}
