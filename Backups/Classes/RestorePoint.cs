using System.Collections.Generic;
using Backups.Exceptions;
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
            _storages = storages ?? throw new BackupsException("Create restore point error: storages can't be null");
            _storageAlgo = storageAlgo ??
                           throw new BackupsException("Create restore point error: storageAlgo can't be null");
            if (storageRepo == null)
            {
                throw new BackupsException("Create restore point error: storageRepo can't be null");
            }

            storageRepo.CreateFolder(path, $"RestorePoint{id}");
        }
    }
}