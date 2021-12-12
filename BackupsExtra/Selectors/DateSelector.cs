using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Backups.Classes;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Selectors
{
    /*[DataContract]*/
    public class DateSelector : IRestorePointsSelector
    {
        public DateSelector(DateTime latestDate)
        {
            LatestDate = latestDate;
        }

        /*[DataMember]*/
        public DateTime LatestDate { get; }

        public LinkedList<RestorePointExtra> SelectRestorePoints(LinkedList<RestorePointExtra> restorePoints)
        {
            var restorePointsToDelete = new LinkedList<RestorePointExtra>();
            foreach (RestorePointExtra restorePoint in restorePoints)
            {
                if (restorePoint.DateTime < LatestDate)
                {
                    restorePointsToDelete.AddLast(restorePoint);
                }
            }

            return restorePointsToDelete;
        }
    }
}