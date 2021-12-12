using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Backups.Classes;
using Backups.Interfaces;
using Backups.Services;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;
using Newtonsoft.Json;

namespace BackupsExtra.Service
{
    public class BackupServiceExtra : BackupServiceDecorator
    {
        /*[DataMember]*/
        public const string ConfigFilePath = @"C:\Users\sergo\Desktop\configFile.txt";
        public BackupServiceExtra(BackupServiceComponent backupServiceComponent)
            : base(backupServiceComponent)
        {
        }

        /*[DataMember]*/
        public new List<BackupJobExtra> BackupJobs { get; private set; } = new ();

        public BackupJobExtra CreateBackupJob(
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string rootPath,
            string backupJobName,
            IRestorePointsSelector selector,
            ILoger logger,
            IRestorePointsUpdater updater)
        {
            var newBackupJob = new BackupJobExtra(
                new BackupJob(
                    storageAlgo,
                    storageRepo,
                    rootPath,
                    backupJobName),
                logger,
                selector,
                updater);
            BackupJobs.Add(newBackupJob);
            return newBackupJob;
        }

        public string Save()
        {
            string jsonString = JsonConvert.SerializeObject(BackupJobs, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });
            StreamWriter writer = File.AppendText(ConfigFilePath);
            writer.WriteLine(jsonString);
            writer.Close();
            return jsonString;
        }

        public void UploadLastState()
        {
            string lastStateJson = File.ReadAllText(ConfigFilePath) /*.Last()*/;
            BackupJobs = JsonConvert.DeserializeObject<List<BackupJobExtra>>(
                    lastStateJson, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    });
            foreach (var backupJob in BackupJobs)
            {
                foreach (var restorePoint in backupJob.RestorePoints)
                {
                    backupJob.CreateRestorePoint(restorePoint);
                }
            }
        }
    }
}