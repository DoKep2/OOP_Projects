using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.StorageRepo
{
    public class FileSystemRepo : IStorageRepo
    {
        public void CreateFolder(string path, string name)
        {
            Directory.CreateDirectory(Path.Combine(path, name));
        }

        public void CreateArchive(List<JobObject> entities, string pathToFolder, string archiveName)
        {
            string zipPath = Path.Combine(pathToFolder, archiveName);
            var zipArchive = new ZipArchive(
                new FileStream(zipPath, FileMode.Create), ZipArchiveMode.Create);
            foreach (JobObject entity in entities)
            {
                zipArchive.CreateEntryFromFile($"{entity.Path}\\{entity.Name}", entity.Name);
            }

            zipArchive.Dispose();
        }

        public void ExtractArchive(string archivePath, string folderPath)
        {
            ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update);
            zipArchive.ExtractToDirectory(folderPath);
        }

        public int GetRestorePointsAmount(string backupJobPath)
        {
            int currentMaxRestorePointNumber = 0;
            foreach (string directoryPath in Directory.GetDirectories(backupJobPath))
            {
                currentMaxRestorePointNumber = Math.Max(
                    currentMaxRestorePointNumber, int.Parse(directoryPath[^1].ToString()));
            }

            return currentMaxRestorePointNumber + 1;
        }

        public int GetStoragesAmount(string restorePointPath)
        {
            int currentMaxStorageNumber = 0;
            int lastNameCharacterIndex = 5;
            foreach (string filePath in Directory.GetFiles(restorePointPath))
            {
                currentMaxStorageNumber = Math.Max(
                    currentMaxStorageNumber, int.Parse(filePath[^lastNameCharacterIndex].ToString()));
            }

            return currentMaxStorageNumber;
        }

        public bool FolderExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}