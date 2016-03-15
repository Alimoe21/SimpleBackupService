using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService
{
    public class SimpleBackupService : ServiceBase, IBackupService
    {
        private DriveObserver Observer;
        public Configuration Config;
        public WebApi WebApi;
        public Logging Log;

        public SimpleBackupService ( )
        {
            ServiceName = "SimpleBackupService";
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        public static void Main ( )
        {
#if DEBUG
            var task = new Task ( ( ) =>
            {
                var serviceMain = new SimpleBackupService ( );
                serviceMain.Run ( );
                Task.Delay ( -1).Wait ( );
            } );
            task.Start();
            task.Wait();
#else
            ServiceBase.Run(new SimpleBackupService());
#endif
        }

        private void Run()
        {
            ServiceComposition.Compose(this);
            Observer = new DriveObserver();
            Observer.DriveConnected += Observer_DriveConnected;
            Observer.Start();
            Log.Info("Service started!");
        }

        protected override void OnStart ( string [ ] args )
        {
            Run ( );
            Log.Info("Service started!");
        }

        protected override void OnCustomCommand ( int command )
        {
            base.OnCustomCommand ( command );
        }

        protected override void OnStop ( )
        {
            Log.Info("Service stoping!");
            Observer?.Dispose();
            base.OnStop ( );
        }

        private void Observer_DriveConnected(object sender, DriveConnectedEventArgs e)
        {
            Log.Info("New Drive connected: {0}", e.Drive?.Name);
            if ( e.Drive != null && e.Drive.IsReady)
            {
                var backupTask = new Task( ( ) =>
                {
                    Log.Debug("Starting Backup Task");
                    var backupScripts = e.Drive.RootDirectory.GetFiles(Config.BackupScriptName);
                    if (backupScripts.Length > 0)
                    {
                        var backupProcess = new Process
                        {
                            StartInfo = new ProcessStartInfo ( "cmd.exe" )
                            {
                                WorkingDirectory = e.Drive.RootDirectory.FullName,
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                RedirectStandardInput = true
                            }
                        };
                        try
                        {
                            backupProcess.Start ( );
                            backupProcess.StandardInput.WriteLine ( backupScripts [ 0 ].FullName );
                            backupProcess.StandardInput.WriteLine ( "exit" );

                            backupProcess.WaitForExit ( );
                        }
                        catch ( Exception ex )
                        {
                            Log.Info ( "Backup failed with exception: {0}", ex.Message );
                        }
                        finally
                        {
                            Log.Debug($"Backupprocess exited with Code {backupProcess.ExitCode}.\n Output:\n{backupProcess.StandardOutput.ReadToEnd()}");
                        }
                        backupProcess.Dispose();
                    }
                    else
                    {
                        Log.Debug("No backup script found.");
                    }
                });
                backupTask.Start();
            }
            else
            {
                Log.Debug("Drive was not ready!");
            }
        }
    }
}
