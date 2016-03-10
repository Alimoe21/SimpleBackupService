using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService
{
    public class DriveObserver : IDisposable
    {
        private ManagementEventWatcher insertWatcher;

        public event EventHandler<DriveConnectedEventArgs> DriveConnected;

        public void Start ( )
        {
            WqlEventQuery insertQuery = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");

            insertWatcher = new ManagementEventWatcher(insertQuery);
            insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
            insertWatcher.Start();
            //var waitForNextEvent = insertWatcher.WaitForNextEvent ( );
            
        }

        private void DeviceInsertedEvent ( object sender, EventArrivedEventArgs e )
        {
            DriveConnected?.Invoke(this, new DriveConnectedEventArgs ( e ));
        }

        public void Dispose ( )
        {
            if (insertWatcher != null)
            {
                insertWatcher.Stop();
                insertWatcher.Dispose();
            }
        }
    }

    public class DriveConnectedEventArgs : EventArgs
    {
        public DriveConnectedEventArgs (EventArrivedEventArgs e)
        {
            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent;
            var driveName = instance.Properties ["DriveName"];
            var drives = DriveInfo.GetDrives ( );

            Drive = drives.FirstOrDefault ( x => x.Name.StartsWith(driveName.Value.ToString()) );
        }

        public DriveInfo Drive { get; private set; }
    }
}
