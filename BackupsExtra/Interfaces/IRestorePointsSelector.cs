using System;
using System.Collections.Generic;
using Backups.Classes;

namespace BackupsExtra.Interfaces
{
    public interface IRestorePointsSelector
    {
        LinkedList<RestorePointExtra> SelectRestorePoints(LinkedList<RestorePointExtra> restorePoints);
    }
}