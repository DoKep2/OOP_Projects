using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Backups.Classes;
using Backups.Interfaces;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;
using IStorageRepo = Backups.Interfaces.IStorageRepo;

namespace BackupsExtra
{
    /*[DataContract]*/
    public class RestorePointExtra : RestorePointDecorator
    {
        public RestorePointExtra(
            RestorePointComponent restorePointComponent,
            ILoger logger,
            DateTime dateTime)
            /*List<StorageExtra> storages)*/
            : base(restorePointComponent)
        {
            Logger = logger;
            DateTime = dateTime;
            Storages = StorageExtra.CreateList(restorePointComponent.Storages, logger, dateTime);
            Id = restorePointComponent.Id;
            Path = new string(restorePointComponent.Path);
            StorageAlgo = restorePointComponent.StorageAlgo;
            StorageRepo = restorePointComponent.StorageRepo;
            Logger.LogInfo("Restore point was created");
        }

        /*public new int Id => RestorePointComponent.Id;
        public new string Path => RestorePointComponent.Path;
        public new IStorageAlgo StorageAlgo => RestorePointComponent.StorageAlgo;
        public new IStorageRepo StorageRepo => RestorePointComponent.StorageRepo;*/
        /*[DataMember]*/
        public new List<StorageExtra> Storages { get; init; }
        /*[DataMember]*/
        public ILoger Logger { get; }
        /*[DataMember]*/
        public DateTime DateTime { get; }
        public StorageExtra FindStorage(StorageExtra storage)
        {
            return Storages.SingleOrDefault(st => Equals(st, storage));
        }
    }
}