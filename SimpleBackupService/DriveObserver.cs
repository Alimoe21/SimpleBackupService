using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBackupService
{
    public class DriveObserver : IDisposable
    {
        private const string wqlQueryString = "SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2";
        private readonly ManagementEventWatcher insertWatcher;
        
        public event EventHandler<DriveConnectedEventArgs> DriveConnected;

        public DriveObserver ( )
        {
            insertWatcher = new ManagementEventWatcher(new WqlEventQuery(wqlQueryString));
            insertWatcher.EventArrived += DeviceInsertedEvent;
        }

        public void Start ( )
        {
            insertWatcher.Start();
        }

        private void DeviceInsertedEvent ( object sender, EventArrivedEventArgs e )
        {
            DriveConnected?.Invoke(this, new DriveConnectedEventArgs ( e ));
        }

        public void Dispose ( )
        {
            if (insertWatcher != null)
            {
                insertWatcher.EventArrived -= DeviceInsertedEvent;
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
