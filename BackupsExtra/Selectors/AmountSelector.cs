using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Backups.Classes;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Selectors
{
    /*[DataContract]*/
    public class AmountSelector : IRestorePointsSelector
    {
        public AmountSelector(int maxRestorePointsAmount)
        {
            MaxRestorePointsAmount = maxRestorePointsAmount;
        }

        /*[DataMember]*/
        public int MaxRestorePointsAmount { get; }

        public LinkedList<RestorePointExtra> SelectRestorePoints(LinkedList<RestorePointExtra> restorePoints)
        {
            var restorePointsToDelete = new LinkedList<RestorePointExtra>();
            int amountToDelete = restorePoints.Count - MaxRestorePointsAmount;
            if (MaxRestorePointsAmount == 0) throw new Exception("Can't delete all restore points");
            int counter = 1;
            foreach (var restorePoint in restorePoints)
            {
                if (counter <= amountToDelete)
                {
                    restorePointsToDelete.AddLast(restorePoint);
                }

                counter++;
            }

            return restorePointsToDelete;
        }
    }
}