using System.Collections.Generic;
using Isu.Classes;

namespace Isu.Interfaces
{
    public interface IStudentsRepository
    {
        Student RegisterStudent(Student student);
        List<Student> GetAll();
        Student Find(int id);
        void Print();
    }
}