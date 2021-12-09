using System;
using System.IO;
using System.Linq;
using System.Threading;
using Backups.Classes;
using Backups.Services;
using Backups.StorageAlgo;
using Backups.StorageRepo;
using BackupsExtra.Logers;
using BackupsExtra.Selectors;
using BackupsExtra.Updaters;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var backupService = new BackupsExtra.Service.BackupServiceExtra();
            /*backupService.UploadLastState();*/
            /*backupService.BackupJobs[0].Update();*/
            /*backupService.BackupJobs[0].Update();*/
            /*backupService.UploadLastState();
            backupService.BackupJobs[0].CreateRestorePoint();
            Console.WriteLine(backupService.BackupJobs[0].RestorePoints.Count);
            backupService.BackupJobs[0].Update();
            Console.WriteLine(backupService.BackupJobs[0].RestorePoints.Count);*/
            /*backupService.BackupJobs[0].CreateRestorePoint();
            backupService.BackupJobs[0].CreateRestorePoint();*/
            /*backupService.BackupJobs[0].Update();*/
            /*Console.WriteLine(backupService.BackupJobs[0].RestorePoints.Count);*/

            /*backupService.BackupJobs[0].*/
            /*int a = backupService.BackupJobs[0].RestorePoints.Last().StorageExtras[0].JobObjects.Count;
            Console.WriteLine(a);*/
            BackupJobExtra backupJob = backupService.CreateBackupJob(
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
            /*backupService.UploadLastState();*/
            /*Console.WriteLine(backupService.BackupJobs[0].RestorePoints[0].DateTime);*/
            /*backupService.Save();*/
        }
    }
}