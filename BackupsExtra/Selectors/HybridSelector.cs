using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Backups.Classes;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Selectors
{
    /*[DataContract]*/
    public class HybridSelector : IRestorePointsSelector
    {
        public HybridSelector(List<IRestorePointsSelector> selectors, HybridMode hybridMode)
        {
            Selectors = selectors;
            HybridMode = hybridMode;
        }

        /*[DataMember]*/
        public List<IRestorePointsSelector> Selectors { get; }
        /*[DataMember]*/
        public HybridMode HybridMode { get; }
        public LinkedList<RestorePointExtra> SelectRestorePoints(
            LinkedList<RestorePointExtra> restorePoints)
        {
            var restorePointsToDelete = new HashSet<RestorePointExtra>();
            if (HybridMode == HybridMode.OneRequirement)
            {
                foreach (IRestorePointsSelector selector in Selectors)
                {
                    LinkedList<RestorePointExtra> selectedRestorePoints = selector.SelectRestorePoints(restorePoints);
                    foreach (RestorePointExtra restorePoint in selectedRestorePoints)
                    {
                        restorePointsToDelete.Add(restorePoint);
                    }
                }
            }
            else if (HybridMode == HybridMode.AllRequirements)
            {
                foreach (IRestorePointsSelector selector in Selectors)
                {
                    if (restorePointsToDelete.Count == 0)
                    {
                        foreach (RestorePointExtra restorePoint in selector.SelectRestorePoints(restorePoints))
                        {
                            restorePointsToDelete.Add(restorePoint);
                        }
                    }
                    else
                    {
                        restorePointsToDelete.IntersectWith(selector.SelectRestorePoints(restorePoints));
                    }
                }
            }

            return new LinkedList<RestorePointExtra>(restorePointsToDelete);
        }
    }
}