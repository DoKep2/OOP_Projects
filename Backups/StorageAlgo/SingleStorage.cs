using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.StorageAlgo
{
    public class SingleStorage : IStorageAlgo
    {
        public List<Storage> CreateStorages(List<JobObject> jobObjects, string pathToRestorePoint, int id)
        {
            return new List<Storage>() { new Storage(jobObjects, pathToRestorePoint, id) };
        }
    }
}