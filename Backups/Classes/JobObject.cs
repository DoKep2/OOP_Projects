using System.Runtime.Serialization;

namespace Backups.Classes
{
    public class JobObject
    {
        public JobObject(string path, string name)
        {
            Path = path;
            Name = name;
        }

        /*[DataMember]*/

        public string Path { get; }
        public string Name { get; }
    }
}