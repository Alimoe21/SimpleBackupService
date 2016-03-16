using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBackupService.Contracts;

namespace SimpleBackupService.Config
{
    [Export(typeof(IBackupConfig))]
    public class BackupConfig : IBackupConfig
    {
        private readonly Configuration _config;

        public string BackupScriptName
        {
            get
            {
                try
                {
                    return _config.AppSettings.Settings [ "BackupScriptName" ].Value;
                }
                catch
                {
                    return "backup.bat";
                }
            }
        }

        public bool EnableDebug
        {
            get
            {
                try
                {
                    return bool.Parse(_config.AppSettings.Settings["EnableDebug"].Value);
                }
                catch
                {
                    return true;
                }
            }
        }

        public string WebApiUrl
        {
            get
            {
                try
                {
                    return _config.AppSettings.Settings["WebApiUrl"].Value;
                }
                catch
                {
                    return "http://localhost:5000";
                }
            }
        }

        public BackupConfig ( )
        {
            var exeConfigurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = typeof ( BackupConfig ).Assembly.Location + ".config" };
            _config = ConfigurationManager.OpenMappedExeConfiguration ( exeConfigurationFileMap, ConfigurationUserLevel.None );
        }
    }
}
