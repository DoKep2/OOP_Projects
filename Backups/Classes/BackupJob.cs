using System.Collections.Generic;
using System.IO;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class BackupJob
    {
        private readonly IStorageAlgo _storageAlgo;
        private readonly List<RestorePoint> _restorePoints;
        private readonly List<JobObject> _jobObjects;

        public BackupJob(
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string rootPath,
            string backupJobName)
        {
            _storageAlgo = storageAlgo;
            StorageRepo = storageRepo;
            _restorePoints = new List<RestorePoint>();
            _jobObjects = new List<JobObject>();
            RootPath = rootPath;
            BackupJobName = backupJobName;
            StorageRepo.CreateFolder(RootPath, backupJobName);
        }

        public string RootPath { get; }
        public string BackupJobName { get; }
        private IStorageRepo StorageRepo { get; }
        private string BackupJobPath => Path.Combine(RootPath, BackupJobName);

        public string CreateRestorePoint()
        {
            int restorePointNumber = StorageRepo.GetRestorePointsAmount(BackupJobPath) + 1;
            string restorePointPath = $"{BackupJobPath}\\RestorePoint{restorePointNumber}";
            List<Storage> storages = _storageAlgo.CreateStorages(_jobObjects);
            var restorePoint = new RestorePoint(storages, _storageAlgo, StorageRepo, BackupJobPath, restorePointNumber);
            _restorePoints.Add(restorePoint);
            foreach (Storage storage in storages)
            {
                int storageNumber = StorageRepo.GetStoragesAmount(restorePointPath) + 1;
                StorageRepo.CreateArchive(
                    storage.JobObjects,
                    restorePointPath,
                    $"Storage{storageNumber}.zip");
            }

            return restorePointPath;
        }

        public JobObject AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            _jobObjects.Remove(jobObject);
        }
    }
}