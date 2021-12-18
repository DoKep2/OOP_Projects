using System.Collections.Generic;
using System.Runtime.Serialization;
using Backups.Classes;

namespace Backups.Interfaces
{
    public abstract class StorageComponent
    {
        public List<JobObject> JobObjects { get; protected init; }
        public string Path { get; protected init; }
        public int Id { get; protected init; }
    }
}