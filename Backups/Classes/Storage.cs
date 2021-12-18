using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class Storage : StorageComponent
    {
        public Storage(List<JobObject> jobObjects, string path, int id)
        {
            JobObjects = new List<JobObject>(jobObjects);
            Path = path;
            Id = id;
        }
    }
}