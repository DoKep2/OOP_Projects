using System;
using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class BackupJob
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

        public IStorageAlgo StorageAlgo { get; }
        public List<JobObject> JobObjects { get; }

        public LinkedList<RestorePoint> RestorePoints { get; protected set; }
        public string RootPath { get; }
        public string BackupJobName { get; }
        public IStorageRepo StorageRepo { get; }
        public string BackupJobPath => Path.Combine(RootPath, BackupJobName);

        public void CreateRestorePoint(RestorePoint restorePoint)
        {
            string restorePointPath = Path.Combine(BackupJobPath, "RestorePoint" + restorePoint.Id);
            StorageRepo?.CreateFolder(BackupJobPath, $"RestorePoint{restorePoint.Id}");
            int storageNumber = 1;
            foreach (Storage storage in restorePoint.Storages)
            {
                StorageRepo.CreateArchive(
                    storage.JobObjects,
                    restorePointPath,
                    $"Storage{storageNumber++}.zip");
            }
        }

        public string CreateRestorePoint()
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
                restorePointNumber,
                DateTime.Now);
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

        public JobObject AddJobObject(JobObject jobObject)
        {
            JobObjects.Add(jobObject);
            return jobObject;
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            JobObjects.Remove(jobObject);
        }
    }
}