using System;
using System.IO;
using System.Linq;
using System.Threading;
using Backups.Classes;
using Backups.Interfaces;
using Backups.Services;
using Backups.StorageAlgo;
using Backups.StorageRepo;
using BackupsExtra.Classes;
using BackupsExtra.Logers;
using BackupsExtra.Selectors;
using BackupsExtra.Service;
using BackupsExtra.Updaters;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var backupService = new BackupServiceExtra(new BackupService());
            backupService.UploadLastState();
            /*Console.WriteLine(backupService.BackupJobs.Count);
            Console.WriteLine(backupService.BackupJobs[0].RestorePoints.First.Value.Storages.Count);*/
            /*Console.WriteLine(backupService.BackupJobs[0]);*/
            /*backupService.BackupJobs[0].Update(backupService.BackupJobs[0].RestorePoints);*/
            /*BackupJobExtra backupJob = backupService.CreateBackupJob(
                new SingleStorage(),
                new FileSystemRepo(),
                @"C:\Users\sergo\Desktop\BACKUPJOBS",
                "jobname1",
                new AmountSelector(1),
                new ConsoleLoger(true),
                new MergeUpdater());
            backupJob.AddJobObject(new JobObject(@"C:\Users\sergo\Desktop\JOBS", "JOB1.txt"));
            backupJob.CreateRestorePoint();
            backupJob.AddJobObject(new JobObject(@"C:\Users\sergo\Desktop\JOBS", "JOB2.txt"));
            backupJob.CreateRestorePoint();
            backupService.Save();
            Console.WriteLine(backupService.BackupJobs);*/
        }
    }
}