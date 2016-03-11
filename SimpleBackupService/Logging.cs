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

        public void LogDebug ( string message, params object [ ] d )
        {
            _backupLogging?.LogDebug(message, d);
        }
    }
}