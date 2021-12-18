using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Principal;
using Backups.Classes;

namespace Backups.Interfaces
{
    public abstract class BackupServiceComponent
    {
        public List<BackupJob> BackupJobs { get; } = new ();

        public abstract BackupJob CreateBackupJob(
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string backupJobName,
            string rootPath);

        public abstract BackupJob GetBackUpJob(string name);
    }
}