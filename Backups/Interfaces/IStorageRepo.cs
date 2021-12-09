using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IStorageRepo
    {
        void CreateFolder(string path, string name);
        void DeleteFolder(string path, string name);
        void CreateArchive(List<JobObject> entities, string pathToFolder, string archiveName);
        void DeleteArchive(string path, string name);
        void ExtractArchive(string archivePath, string folderPath);
        int GetRestorePointsAmount(string backupJobPath);
        int GetStoragesAmount(string restorePointPath);
        bool FileExists(string path);
        bool FolderExists(string path);
    }
}