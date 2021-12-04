using System;
using System.Linq;
using Isu.Classes;
using Isu.Repositories;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IsuService _isuService;
        private readonly GroupsRepository _groupsRepository = new GroupsRepository();
        private readonly StudentsRepository _studentsRepository = new StudentsRepository();

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService(_groupsRepository, _studentsRepository);
            
        }
        
        [TestCase("Petya", 2, 2)]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string studentName, int groupNumber,
            int courseNumber)
        {
            Group group = _isuService.AddGroup(new Group(courseNumber, groupNumber));
            _isuService.AddStudent(group, studentName);
            Student newStudent = _isuService.FindStudent(studentName);
            CollectionAssert.Contains(_studentsRepository.GetAll().
                Where(currentStudent => currentStudent.Group == group), newStudent);
        }

        [TestCase(5, 2, "Petya")]
        public void ReachMaxStudentPerGroup_ThrowException(int groupNumber, int courseNumber, string studentName)
        { 
            Assert.Catch<IsuException>(() =>
            {
                var group = new Group(courseNumber, groupNumber);
                _isuService.AddGroup(group);
                for (int i = 0; i < 26; i++)
                {
                    _isuService.AddStudent(group, studentName);
                    Console.WriteLine(_isuService.GetGroupSize(group));
                }
            });
        }

        [TestCase(10, 6) ]
        [TestCase(-5, 2)]
        public void CreateGroupWithInvalidName_ThrowException(int courseNumber, int groupNumber)
        {
            Assert.Catch<Exception>(() =>
                new Group(courseNumber, groupNumber));
        }

        [TestCase(1, 1, "Vitya")]
        public void TransferStudentToAnotherGroup_GroupChanged(int courseNumber, int groupNumber, string studentName)
        {
            var newGroup = new Group(courseNumber, groupNumber);
            Student student = _isuService.AddStudent(newGroup, studentName);
            _isuService.ChangeStudentGroup(student, newGroup);
            CollectionAssert.Contains(_studentsRepository.GetAll()
                .Where(currentStudent => currentStudent.Group == newGroup), student);
        }
    }
}