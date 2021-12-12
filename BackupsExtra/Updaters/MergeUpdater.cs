using System;
using System.Collections.Generic;
using Backups.Classes;
using Backups.StorageAlgo;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Updaters
{
    public class MergeUpdater : IRestorePointsUpdater
    {
        public void Update(LinkedList<RestorePointExtra> restorePoints, int amountToUpdate)
        {
            int counter = 0;
            RestorePointExtra newRestorePoint = null;
            foreach (RestorePointExtra restorePoint in restorePoints)
            {
                if (counter == amountToUpdate)
                {
                    newRestorePoint = restorePoint;
                    break;
                }

                counter++;
            }

            counter = 0;
            var copy = new LinkedList<RestorePointExtra>(restorePoints);
            foreach (RestorePointExtra restorePoint in copy)
            {
                if (restorePoint == newRestorePoint)
                {
                    break;
                }

                var copy2 = new List<StorageExtra>(restorePoint.Storages); /* !!!!!!!!!!!!!!!!!!!!!!!!!!*/
                foreach (StorageExtra storage in copy2)
                {
                    if (newRestorePoint?.FindStorage(storage) == null)
                    {
                        StorageExtra.CreateList(
                            newRestorePoint?.StorageAlgo.CreateStorages(
                            storage.StorageComponent.JobObjects,
                            newRestorePoint./*RestorePointComponent.*/Path,
                            newRestorePoint./*RestorePointComponent.*/StorageRepo.GetStoragesAmount(newRestorePoint./*RestorePointComponent.*/Path) + 1),
                            storage.Logger,
                            storage.DateTime);
                    }

                    restorePoint.Storages.Remove(storage);
                    restorePoint./*RestorePointComponent.*/StorageRepo.DeleteArchive(
                        storage./*StorageComponent.*/Path,
                        $"StorageExtra{storage./*StorageComponent.*/Id}.zip");
                    if (restorePoint.StorageAlgo.GetType() == typeof(SingleStorage))
                    {
                        restorePoints.Remove(restorePoint);
                        restorePoint./*RestorePointComponent.*/StorageRepo.DeleteFolder(
                            restorePoint./*RestorePointComponent.*/Path,
                            $"RestorePoint{restorePoint./*RestorePointComponent.*/Id}");
                    }

                    counter++;
                    if (counter == amountToUpdate)
                    {
                        break;
                    }
                }
            }
        }
    }
}