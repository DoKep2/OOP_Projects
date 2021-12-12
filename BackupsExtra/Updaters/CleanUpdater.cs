using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Updaters
{
    public class CleanUpdater : IRestorePointsUpdater
    {
        public void Update(LinkedList<RestorePointExtra> restorePoints, int amountToUpdate)
        {
            for (int i = 0; i < amountToUpdate; i++)
            {
                string restorePointPath = restorePoints.First.Value.Path;
                string restorePointName = $"RestorePoint{restorePoints.First.Value.Id}";
                restorePoints.First.Value.StorageRepo.DeleteFolder(restorePointPath, restorePointName);
                restorePoints.RemoveFirst();
            }
        }
    }
}