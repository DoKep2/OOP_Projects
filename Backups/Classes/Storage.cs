using System.Collections.Generic;

namespace Backups.Classes
{
    public class Storage
    {
        public Storage(List<JobObject> jobObjects)
        {
            JobObjects = jobObjects;
        }

        public List<JobObject> JobObjects { get; }
    }
}