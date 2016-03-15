using SimpleBackupService.Contracts;

namespace SimpleBackupService
{
    public class Logging : IBackupLogging
    {
        private readonly IBackupLogging _backupLogging;

        public Logging(IBackupLogging backupLogging)
        {
            this._backupLogging = backupLogging;
        }

        public void Debug ( string message, params object [ ] args )
        {
            _backupLogging?.Debug(message, args);
        }

        public void Info ( string message, params object [ ] args )
        {
            _backupLogging?.Info(message, args);
        }
    }
}