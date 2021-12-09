using System.Collections.Generic;

namespace Backups.Classes
{
    public class Storage
    {
        public Storage(List<JobObject> jobObjects, string path, int id)
        {
            JobObjects = new List<JobObject>(jobObjects);
            Path = path;
            Id = id;
        }

        public List<JobObject> JobObjects { get; }
        public string Path { get; }
        public int Id { get; }
    }
}