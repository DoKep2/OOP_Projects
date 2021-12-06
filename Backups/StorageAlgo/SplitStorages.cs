using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.StorageAlgo
{
    public class SplitStorages : IStorageAlgo
    {
        private readonly List<JobObject> _jobObjects = new List<JobObject>();
        public List<Storage> CreateStorages(List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();
            for (int i = 0; i < jobObjects.Count; i++)
            {
                storages.Add(new Storage(new List<JobObject>() { jobObjects[i] }));
            }

            return storages;
        }
    }
}