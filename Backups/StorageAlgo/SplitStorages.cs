using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.StorageAlgo
{
    public class SplitStorages : IStorageAlgo
    {
        public List<Storage> CreateStorages(List<JobObject> jobObjects, string pathToRestorePoint, int id)
        {
            var storages = new List<Storage>();
            for (int i = 0; i < jobObjects.Count; i++)
            {
                storages.Add(new Storage(new List<JobObject>() { jobObjects[i] }, pathToRestorePoint, id++));
            }

            return storages;
        }
    }
}