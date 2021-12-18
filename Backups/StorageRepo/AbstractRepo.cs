using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.StorageRepo
{
    public class AbstractRepo : IStorageRepo
    {
        public List<string> Folders { get; } = new List<string>();
        public List<string> Files { get; } = new List<string>();

        public void CreateFolder(string path, string name)
        {
            Folders.Add(Path.Combine(path, name));
        }

        public void DeleteFolder(string path, string name)
        {
            string fullPath = Path.Combine(path, name);
            if (!FolderExists(fullPath))
            {
                throw new Exception("No such folder to delete");
            }

            Folders.Remove(fullPath);
            foreach (string file in Files)
            {
                if (file.StartsWith(fullPath))
                {
                    Files.Remove(file);
                }
            }
        }

        public void DeleteArchive(string path, string name)
        {
            string fullPath = Path.Combine(path, name);
            if (!FileExists(fullPath))
            {
                throw new Exception("No such file to delete");
            }

            Files.Remove(fullPath);
        }

        public void CreateArchive(List<JobObject> entities, string pathToFolder, string archiveName)
        {
            string archivePath = Path.Combine(pathToFolder, archiveName);
            Files.Add(archivePath);
            foreach (JobObject jobObject in entities)
            {
                Files.Add(Path.Combine(archivePath, jobObject.Name));
            }
        }

        public void ExtractArchive(string archivePath, string folderPath)
        {
            var newFiles = new List<string>();
            foreach (string filePath in Files)
            {
                if (filePath.StartsWith(archivePath) && !filePath.Equals(archivePath))
                {
                    string fileName = Path.GetFileName(filePath);
                    newFiles.Add(Path.Combine(folderPath, fileName));
                }
            }

            foreach (string newFile in newFiles)
            {
                Files.Add(newFile);
            }
        }

        public int GetRestorePointsAmount(string backupJobPath)
        {
            int restorePointsMaxNumber = 0;
            foreach (string folder in Folders)
            {
                if (folder.StartsWith(backupJobPath) && !folder.Equals(backupJobPath))
                {
                    restorePointsMaxNumber = Math.Max(
                        restorePointsMaxNumber, int.Parse(folder[^1].ToString()));
                }
            }

            return restorePointsMaxNumber;
        }

        public int GetStoragesAmount(string restorePointPath)
        {
            int storageMaxNumber = 0;
            foreach (string file in Files)
            {
                if (file.StartsWith(restorePointPath) && file.EndsWith(".zip"))
                {
                    storageMaxNumber = Math.Max(
                        storageMaxNumber, int.Parse(file[^5].ToString()));
                }
            }

            return storageMaxNumber;
        }

        public bool FolderExists(string path)
        {
            return Folders.Contains(path);
        }

        public bool FileExists(string path)
        {
            return Files.Contains(path);
        }
    }
}