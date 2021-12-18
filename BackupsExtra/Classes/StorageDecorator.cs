using System.Runtime.Serialization;
using Backups.Interfaces;

namespace BackupsExtra.Classes
{
    /*[DataContract]*/
    public abstract class StorageDecorator : StorageComponent
    {
        public StorageDecorator(StorageComponent storageComponent)
        {
            StorageComponent = storageComponent;
        }

        /*[DataMember]*/

        public StorageComponent StorageComponent { get; }
    }
}