using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Services
{
    public class BackupService
    {
        private readonly List<BackupJob> _backupJobs = new ();
        public BackupJob CreateBackupJob(
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string backupJobName,
            string rootPath)
        {
            var newBackupJob = new BackupJob(storageAlgo, storageRepo, rootPath, backupJobName);
            _backupJobs.Add(newBackupJob);
            return newBackupJob;
        }

        public BackupJob GetBackUpJob(string name)
        {
            foreach (BackupJob currentBackupJob in _backupJobs
                .Where(currentBackupJob => name.Equals(currentBackupJob.BackupJobName)))
            {
                return currentBackupJob;
            }

            throw new BackupsException("Getting backup job error: no such backup job");
        }
    }
}