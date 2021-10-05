using System.Dynamic;
using System.Net.NetworkInformation;

namespace Isu.Services
{
    public class Student
    {
        private static int _curId = 0;

        public Student(string name)
        {
            Id = _curId;
            Name = name;
            NameGroup = null;
            _curId++;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public GroupName NameGroup { get; set; }
        public override string ToString()
        {
            return $"{string.Format("{0:D4}", Id)}, {Name}, {NameGroup}";
        }
    }
}