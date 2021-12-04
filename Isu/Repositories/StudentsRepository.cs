using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Interfaces;

namespace Isu.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly List<Student> _studentsList;

        public StudentsRepository()
        {
            _studentsList = new List<Student>();
        }

        public Student RegisterStudent(Student student)
        {
            _studentsList.Add(student);
            return student;
        }

        public List<Student> GetAll()
        {
            return new List<Student>(_studentsList);
        }

        public Student Find(int id)
        {
            return _studentsList.FirstOrDefault(currentStudent => currentStudent.Id == id);
        }

        public Student Find(string name)
        {
            return _studentsList.FirstOrDefault(currentStudent => currentStudent.Name.Equals(name));
        }

        public void Print()
        {
            foreach (Student currentStudent in _studentsList)
            {
                Console.WriteLine($"Student id: {currentStudent.Id}," +
                                  $" name: {currentStudent.Name}," +
                                  $" group: {currentStudent.Group}");
            }
        }
    }
}