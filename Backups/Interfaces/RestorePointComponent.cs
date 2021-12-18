using System.Collections.Generic;
using System.Runtime.Serialization;
using Backups.Classes;

namespace Backups.Interfaces
{
    public abstract class RestorePointComponent
    {
        public List<Storage> Storages { get; init; }
        public IStorageAlgo StorageAlgo { get; init; }
        public IStorageRepo StorageRepo { get; init; }
        public string Path { get; init; }
        public int Id { get; init; }
    }
}