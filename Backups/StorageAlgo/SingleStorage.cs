using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.StorageAlgo
{
    public class SingleStorage : IStorageAlgo
    {
        private readonly List<JobObject> _jobObjects = new List<JobObject>();

        public List<Storage> CreateStorages(List<JobObject> jobObjects)
        {
            return new List<Storage>() { new Storage(jobObjects) };
        }
    }
}