using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class RestorePoint : RestorePointComponent
    {
        public RestorePoint(
            List<Storage> storages,
            IStorageAlgo storageAlgo,
            IStorageRepo storageRepo,
            string path,
            int id)
        {
            Storages = storages; /*?? throw new BackupsException("Create restore point error: storages can't be null");*/
            StorageAlgo = storageAlgo;
            StorageRepo = storageRepo;
            Id = id;
            Path = path; /*??
                           throw new BackupsException("Create restore point error: storageAlgo can't be null");*/
            /*if (storageRepo == null)
            {
                throw new BackupsException("Create restore point error: storageRepo can't be null");
            }*/
            /*storageRepo?.CreateFolder(path, $"RestorePoint{id}");*/
        }

        /*public Storage FindStorage(Storage storage)
        {
            return Storages.SingleOrDefault(st => Equals(st, storage));
        }*/
    }
}