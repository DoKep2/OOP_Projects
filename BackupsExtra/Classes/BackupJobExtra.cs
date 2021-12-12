using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using Backups.Classes;
using Backups.Interfaces;
using Backups.StorageAlgo;
using BackupsExtra.Interfaces;
using Newtonsoft.Json;

namespace BackupsExtra.Classes
{
    /*[DataContract]*/
    public class BackupJobExtra : BackupJobDecorator
    {
        public BackupJobExtra(
            BackupJobComponent backupJob,
            ILoger logger,
            IRestorePointsSelector restorePointsSelector,
            IRestorePointsUpdater restorePointsUpdater)
            : base(backupJob)
        {
            RestorePointsSelector = restorePointsSelector;
            Logger = logger;
            RestorePointsUpdater = restorePointsUpdater;
            JobObjects = BackupJob.JobObjects;
            RootPath = new string(BackupJob.RootPath);
            StorageAlgo = BackupJob.StorageAlgo;
            StorageRepo = BackupJob.StorageRepo;
            BackupJobName = new string(BackupJob.BackupJobName);
            BackupJobPath = new string(BackupJob.BackupJobPath);
        }

        /*[DataMember]*/
        public ILoger Logger { get; }
        /*private readonly IRestoreMode _restoreMode;*/
        /*[DataMember]*/
        public IRestorePointsSelector RestorePointsSelector { get; }

        /*public new string RootPath => BackupJob.RootPath;
        public new IStorageAlgo StorageAlgo => BackupJob.StorageAlgo;
        public new IStorageRepo StorageRepo => BackupJob.StorageRepo;
        public new string BackupJobName => BackupJob.BackupJobName;
        public new string BackupJobPath => BackupJob.BackupJobPath;*/
        /*[DataMember]*/
        public IRestorePointsUpdater RestorePointsUpdater { get; }
        /*[DataMember]*/
        public new LinkedList<RestorePointExtra> RestorePoints { get; } = new ();

        public void CreateRestorePoint(RestorePointExtra restorePoint)
        {
            string restorePointPath = Path.Combine(BackupJobPath, "RestorePoint" + restorePoint.Id);
            StorageRepo?.CreateFolder(BackupJobPath, $"RestorePoint{restorePoint.Id}");
            int storageNumber = 1;
            foreach (StorageExtra storage in restorePoint.Storages)
            {
                StorageRepo.CreateArchive(
                    storage.JobObjects,
                    restorePointPath,
                    $"Storage{storageNumber++}.zip");
            }
        }

        public new string CreateRestorePoint()
        {
            int restorePointNumber = StorageRepo.GetRestorePointsAmount(BackupJobPath) + 1;
            string restorePointPath = Path.Combine(BackupJobPath, $"RestorePoint{restorePointNumber}");
            StorageRepo?.CreateFolder(BackupJobPath, $"RestorePoint{restorePointNumber}");
            int storageNumber = StorageRepo.GetStoragesAmount(restorePointPath) + 1;
            List<Storage> storages = StorageAlgo.CreateStorages(JobObjects, restorePointPath, storageNumber);
            var restorePoint = new RestorePointExtra(
                new RestorePoint(
                storages,
                StorageAlgo,
                StorageRepo,
                BackupJobPath,
                restorePointNumber),
                Logger,
                DateTime.Now);
            RestorePoints.AddLast(restorePoint);
            foreach (StorageExtra storage in restorePoint.Storages)
            {
                StorageRepo.CreateArchive(
                    storage.JobObjects,
                    restorePointPath,
                    $"Storage{storageNumber++}.zip");
            }

            return restorePointPath;
        }

        public LinkedList<RestorePointExtra> Select(LinkedList<RestorePointExtra> restorePointsExtra)
        {
           return RestorePointsSelector.SelectRestorePoints(restorePointsExtra);
        }

        public void Update(LinkedList<RestorePointExtra> restorePointsExtra)
        {
            LinkedList<RestorePointExtra> selectedPoints = Select(restorePointsExtra);
            RestorePointsUpdater.Update(selectedPoints, selectedPoints.Count);
        }
    }
}