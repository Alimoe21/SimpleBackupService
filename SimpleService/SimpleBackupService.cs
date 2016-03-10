using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SimpleBackupService
{
    public class SimpleBackupService : ServiceBase
    {
        private DriveObserver Observer;

        public SimpleBackupService ( )
        {
            ServiceName = "SimpleBackupService";
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        public static void Main ( )
        {
//#if DEBUG
//            var task = new Task ( ( ) =>
//            {
//                var serviceMain = new SimpleBackupService();
//                serviceMain.Run();
//                Task.Delay(TimeSpan.MaxValue).Wait();
//            } );
//            task.Start();
//            task.Wait ( );
//#else
            ServiceBase.Run(new SimpleBackupService());
//#endif
        }

        private void Run()
        {
            Observer = new DriveObserver();
            Observer.DriveConnected += Observer_DriveConnected;
            Observer.Start();
        }

        protected override void OnStart ( string [ ] args )
        {
            Run ( );

            var configuration = new HttpSelfHostConfiguration ( "http://localhost:5000" );

            configuration.Routes.MapHttpRoute (
                name: "API",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var httpSelfHostServer = new HttpSelfHostServer ( configuration );
            httpSelfHostServer.OpenAsync ( ).Wait ( );
        }

        protected override void OnCustomCommand ( int command )
        {
            base.OnCustomCommand ( command );
        }

        protected override void OnStop ( )
        {
            Observer?.Dispose();
            base.OnStop ( );
        }

        private void Observer_DriveConnected(object sender, DriveConnectedEventArgs e)
        {
            EventLog.WriteEntry($"DriveConnected {e.Drive?.Name}");
            if ( e.Drive != null && e.Drive.IsReady)
            {
                var backupTask = new Task( ( ) =>
                {
                    var backupScripts = e.Drive.RootDirectory.GetFiles("backup.bat");
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
                            backupProcess.Start();
                            backupProcess.StandardInput.WriteLine(backupScripts[0].FullName);
                            backupProcess.StandardInput.WriteLine("exit");

                            backupProcess.WaitForExit();
                        }
                        finally
                        {
                            EventLog.WriteEntry($"Backupprocess exited with Code {backupProcess.ExitCode}.\n Output:\n{backupProcess.StandardOutput.ReadToEnd()}");
                        }
                        backupProcess.Dispose();
                    }
                });
                backupTask.Start();
                
            }
        }
    }
}
