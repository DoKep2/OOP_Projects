using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Interfaces;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int GROUPMAXSIZE = 25;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IStudentsRepository _studentsRepository;

        public IsuService(IGroupsRepository groupsRepository, IStudentsRepository studentsRepository)
        {
            _groupsRepository = groupsRepository;
            _studentsRepository = studentsRepository;
        }

        public List<Group> GetGroupsList()
        {
            return _groupsRepository.GetAll();
        }

        public Group AddGroup(Group group)
        {
            _groupsRepository.RegisterGroup(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (GetGroupSize(group) == GROUPMAXSIZE)
            {
                throw new IsuException("Group is full");
            }

            var newStudent = new Student(name);
            newStudent.Group = group;
            _studentsRepository.RegisterStudent(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            foreach (Student currentStudent in _studentsRepository.GetAll()
                .Where(currentStudent => currentStudent.Id == id))
            {
                return currentStudent;
            }

            throw new IsuException("Student not found");
        }

        public Student FindStudent(string name)
        {
            return _studentsRepository.GetAll()
                .FirstOrDefault(currentStudent => currentStudent.Name.Equals(name));
        }

        public List<Student> FindStudents(Group groupName)
        {
            return _studentsRepository.GetAll()
                .Where(currentStudent => currentStudent.Group.ToString().Equals(groupName.ToString())).ToList();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _studentsRepository.GetAll()
                .Where(curStudent => curStudent.Group.CourseNumber == courseNumber).ToList();
        }

        public Group FindGroup(Group groupName)
        {
            return _groupsRepository.GetAll()
                .FirstOrDefault(curGroup => curGroup.ToString().Equals(groupName.ToString()));
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groupsRepository.GetAll().Where(curGroup => curGroup.CourseNumber == courseNumber).ToList();
        }

        public int GetGroupSize(Group group)
        {
            return _studentsRepository.GetAll().Count(currentStudent => currentStudent.Group == group);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            int newGroupSize = GetGroupSize(newGroup);
            if (newGroupSize == GROUPMAXSIZE)
            {
                throw new IsuException("Group is full");
            }

            foreach (Student currentStudent in _studentsRepository.GetAll()
                .Where(currentStudent => currentStudent.Id == student.Id))
            {
                student.Group = newGroup;
            }
        }
    }
}