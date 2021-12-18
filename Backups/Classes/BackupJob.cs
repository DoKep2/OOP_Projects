using System;
using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class BackupJob : BackupJobComponent
    {
        public BackupJob(
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string rootPath,
            string backupJobName)
        {
            StorageAlgo = storageAlgo;
            StorageRepo = storageRepo;
            RestorePoints = new LinkedList<RestorePoint>();
            JobObjects = new List<JobObject>();
            RootPath = rootPath;
            BackupJobName = backupJobName;
            StorageRepo?.CreateFolder(RootPath, backupJobName);
        }

        public override string CreateRestorePoint()
        {
            int restorePointNumber = StorageRepo.GetRestorePointsAmount(BackupJobPath) + 1;
            string restorePointPath = Path.Combine(BackupJobPath, $"RestorePoint{restorePointNumber}");
            StorageRepo?.CreateFolder(BackupJobPath, $"RestorePoint{restorePointNumber}");
            int storageNumber = StorageRepo.GetStoragesAmount(restorePointPath) + 1;
            List<Storage> storages = StorageAlgo.CreateStorages(JobObjects, restorePointPath, storageNumber);
            var restorePoint = new RestorePoint(
                storages,
                StorageAlgo,
                StorageRepo,
                BackupJobPath,
                restorePointNumber);
            RestorePoints.AddLast(restorePoint);
            foreach (Storage storage in storages)
            {
                StorageRepo.CreateArchive(
                    storage.JobObjects,
                    restorePointPath,
                    $"Storage{storageNumber++}.zip");
            }

            return restorePointPath;
        }

        public override JobObject AddJobObject(JobObject jobObject)
        {
            JobObjects.Add(jobObject);
            return jobObject;
        }

        public override void DeleteJobObject(JobObject jobObject)
        {
            JobObjects.Remove(jobObject);
        }
    }
}