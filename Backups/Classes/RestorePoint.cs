using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private List<Storage> _storages;
        private IStorageAlgo _storageAlgo;

        public RestorePoint(
            List<Storage> storages,
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string path,
            int id)
        {
            _storages = storages;
            _storageAlgo = storageAlgo;
            storageRepo.CreateFolder(path, $"RestorePoint{id}");
        }
    }
}