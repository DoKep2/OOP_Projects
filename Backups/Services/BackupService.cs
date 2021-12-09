using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Services
{
    public class BackupService
    {
        public List<BackupJob> BackupJobs { get; } = new ();
        public BackupJob CreateBackupJob(
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string backupJobName,
            string rootPath)
        {
            var newBackupJob = new BackupJob(storageAlgo, storageRepo, rootPath, backupJobName);
            BackupJobs.Add(newBackupJob);
            return newBackupJob;
        }

        public BackupJob GetBackUpJob(string name)
        {
            foreach (BackupJob currentBackupJob in BackupJobs
                .Where(currentBackupJob => name.Equals(currentBackupJob.BackupJobName)))
            {
                return currentBackupJob;
            }

            throw new BackupsException("Getting backup job error: no such backup job");
        }
    }
}