using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Backups.Classes;
using Backups.Interfaces;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes
{
    /*[DataContract]*/
    public class StorageExtra : StorageDecorator
    {
        public StorageExtra(StorageComponent storageComponent, ILoger logger, DateTime dateTime)
            : base(storageComponent)
        {
            Logger = logger;
            DateTime = dateTime;
            Id = storageComponent.Id;
            Path = new string(storageComponent.Path);
            JobObjects = storageComponent.JobObjects;
            Logger.LogInfo("Storage was created");
        }

        /*[DataMember]*/
        public ILoger Logger { get; }
        /*[DataMember]*/
        public DateTime DateTime { get; }
        /*public new int Id => StorageComponent.Id;
        public new string Path => StorageComponent.Path;
        public new List<JobObject> JobObjects => StorageComponent.JobObjects;*/

        public static List<StorageExtra> CreateList(List<Storage> storages, ILoger logger, DateTime dateTime)
        {
            return storages.Select(storage => new StorageExtra(storage, logger, dateTime)).ToList();
        }
    }
}