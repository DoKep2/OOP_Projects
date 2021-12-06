namespace Backups.Classes
{
    public class JobObject
    {
        public JobObject(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; }
        public string Name { get; }
    }
}