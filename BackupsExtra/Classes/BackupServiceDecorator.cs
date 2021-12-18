using Backups.Classes;
using Backups.Interfaces;

namespace BackupsExtra.Classes
{
    public abstract class BackupServiceDecorator : BackupServiceComponent
    {
        public BackupServiceDecorator(BackupServiceComponent backupServiceComponent)
        {
            BackupServiceComponent = backupServiceComponent;
        }

        public BackupServiceComponent BackupServiceComponent { get; }
        public override BackupJob CreateBackupJob(
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string backupJobName,
            string rootPath)
        {
            return BackupServiceComponent.CreateBackupJob(
                storageAlgo,
                storageRepo,
                backupJobName,
                rootPath);
        }

        public override BackupJob GetBackUpJob(string name)
        {
            return BackupServiceComponent.GetBackUpJob(name);
        }
    }
}