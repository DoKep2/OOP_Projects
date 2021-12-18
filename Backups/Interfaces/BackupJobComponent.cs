using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Backups.Classes;
namespace Backups.Interfaces
{
    public abstract class BackupJobComponent
    {
        private string _backupJobPath;
        public string BackupJobName { get; protected init; }
        public LinkedList<RestorePoint> RestorePoints { get; init; }
        public IStorageAlgo StorageAlgo { get; init; }
        public List<JobObject> JobObjects { get; init; }
        public IStorageRepo StorageRepo { get; init; }
        public string RootPath { get; init; }
        public string BackupJobPath
        {
            get => RootPath == null || BackupJobName == null ? null : Path.Combine(RootPath, BackupJobName);
            init => _backupJobPath = value;
        }

        public abstract string CreateRestorePoint();
        public abstract JobObject AddJobObject(JobObject jobObject);
        public abstract void DeleteJobObject(JobObject jobObject);
    }
}