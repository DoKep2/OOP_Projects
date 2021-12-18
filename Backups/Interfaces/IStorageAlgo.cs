using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IStorageAlgo
    {
        List<Storage> CreateStorages(List<JobObject> jobObjects, string pathToRestorePoint, int id);
    }
}