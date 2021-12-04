using System.Collections.Generic;
using Isu.Classes;

namespace Isu.Interfaces
{
    public interface IIsuService
    {
        Group AddGroup(Group group);
        Student AddStudent(Group group, string name);
        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(Group groupName);
        List<Student> FindStudents(CourseNumber courseNumber);
        Group FindGroup(Group groupName);
        List<Group> FindGroups(CourseNumber courseNumber);
        void ChangeStudentGroup(Student student, Group newGroup);
    }
}