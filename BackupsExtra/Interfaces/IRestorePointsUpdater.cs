using System.Collections.Generic;
using Backups.Classes;

namespace BackupsExtra.Interfaces
{
    public interface IRestorePointsUpdater
    {
        void Update(LinkedList<RestorePointExtra> restorePoints, int amountToUpdate);
    }
}