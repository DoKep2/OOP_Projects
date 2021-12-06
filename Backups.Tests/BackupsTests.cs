using System.IO;
using Backups.Classes;
using Backups.Interfaces;
using Backups.Services;
using Backups.StorageAlgo;
using Backups.StorageRepo;
using NUnit.Framework;
namespace Backups.Tests

{
    [TestFixture]
    public class BackupsTests
    {
        private IStorageAlgo _storageAlgo;
        private IStorageRepo _storageRepo;
        private BackupService _backupService;
        private const string RootPath = @"C:\Users\sergo\Desktop\Root";
        [SetUp]
        public void Setup()
        {
            _storageAlgo = new SingleStorage();
            _storageRepo = new AbstractRepo();
            _backupService = new BackupService();
            
        }

        [Test]
        public void CreateBackupJob()
        {
            _backupService.CreateBackupJob(_storageAlgo, _storageRepo, "BackupJob1", RootPath);
            Assert.IsTrue(_storageRepo.FolderExists(Path.Combine(RootPath, "BackupJob1")));
        }
        [Test]
        public void CreateRestorePoints_WithSingleAlgo_CreatedOneStorage()
        {
            BackupJob backupJob = _backupService.CreateBackupJob(
                new SingleStorage(),
                _storageRepo,
                "BackupJob1",
                RootPath);
            backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName1.txt"));
            backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName2.txt"));
            backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName3.txt"));
            string restorePointPath = backupJob.CreateRestorePoint();
            Assert.AreEqual(_storageRepo.GetStoragesAmount(restorePointPath), 1);
        }
        [Test]
        public void CreateRestorePoint_WithSplitAlgo_CreatedSeveralStorages()
        {
            BackupJob backupJob = _backupService.CreateBackupJob(
                new SplitStorages(),
                _storageRepo,
                "BackupJob1",
                RootPath);
            JobObject jobObject1 = backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName1.txt"));
            JobObject jobObject2 = backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName2.txt"));
            JobObject jobObject3 = backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName3.txt"));
            
            string restorePointPath1 = backupJob.CreateRestorePoint();
            Assert.AreEqual(_storageRepo.GetStoragesAmount(restorePointPath1), 3);
            backupJob.DeleteJobObject(jobObject1);
            string restorePointPath2 = backupJob.CreateRestorePoint();
            Assert.AreEqual(_storageRepo.GetStoragesAmount(restorePointPath2), 2);
        }

        [Test]
        public void ExtractStorage_StorageNotChanged_FolderHasSameFiles()
        {
            BackupJob backupJob = _backupService.CreateBackupJob(
                new SingleStorage(),
                _storageRepo,
                "BackupJob1",
                RootPath);
            JobObject jobObject1 = backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName1.txt"));
            JobObject jobObject2 = backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName2.txt"));
            JobObject jobObject3 = backupJob.AddJobObject(new JobObject(@"C:\somePath", "someName3.txt"));
            string restorePointPath = backupJob.CreateRestorePoint();
            string someFolderPath = @"C:\someFolder";
            _storageRepo.ExtractArchive(Path.Combine(restorePointPath, "Storage1.zip"), someFolderPath);
            Assert.IsTrue(_storageRepo.FileExists(@"C:\someFolder\someName1.txt")  
                          && _storageRepo.FileExists(@"C:\someFolder\someName2.txt")
                          && _storageRepo.FileExists(@"C:\someFolder\someName3.txt"));
            Assert.IsTrue(_storageRepo.FileExists(Path.Combine(restorePointPath, "Storage1.zip", "someName1.txt"))
            && _storageRepo.FileExists(Path.Combine(restorePointPath, "Storage1.zip", "someName2.txt"))
            && _storageRepo.FileExists(Path.Combine(restorePointPath, "Storage1.zip", "someName3.txt")));
        }
    }
}