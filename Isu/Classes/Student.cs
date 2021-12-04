namespace Isu.Classes
{
    public class Student
    {
        private static int _curId = 0;

        public Student(string name)
        {
            Id = _curId;
            Name = name;
            Group = null;
            _curId++;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public Group Group { get; set; }
        public override string ToString()
        {
            return $"{Id:D4}, {Name}, {Group}";
        }
    }
}