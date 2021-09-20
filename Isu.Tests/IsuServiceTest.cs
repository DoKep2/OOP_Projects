using System;
using Isu.Services;
using Isu.Tools;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.DataCollection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Isu.Tests
{
    public class Tests
    {
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
            
        }
        
        [TestCase("Petya", 2, 2)]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string studentName, int groupNumber,
            int courseNumber)
        {
            Group group = _isuService.AddGroup(new GroupName(courseNumber, groupNumber));;
            _isuService.AddStudent(group, studentName);
            Student newStudent = _isuService.FindStudent(studentName);
            if (newStudent.NameGroup == group.GroupName)
            {
                foreach (Student curStudent in group.StudentList)
                {
                    if (curStudent.Name == studentName)
                    {
                        return;
                    }
                }
            }
            Assert.Fail();
        }

        [TestCase(5, 7, "Petya")]
        public void ReachMaxStudentPerGroup_ThrowException(int groupNumber, int courseNumber, string studentName)
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = new Group(courseNumber, groupNumber);
                _isuService.AddGroup(group.GroupName);
                for (int i = 0; i < 26; i++)
                {
                    _isuService.AddStudent(group, studentName);
                }
            });
        }

        [TestCase(10, 6) ]
        [TestCase(-5, 2)]
        [TestCase(3, 4)]
        public void CreateGroupWithInvalidName_ThrowException(int courseNumber, int groupNumber)
        {
            new Group(courseNumber, groupNumber);
        }

        [TestCase(9, 55, "Vitya")]
        public void TransferStudentToAnotherGroup_GroupChanged(int courseNumber, int groupNumber, string studentName)
        {
            Group newGroup = new Group(courseNumber, groupNumber);
            Student student = _isuService.AddStudent(newGroup, studentName);
            _isuService.ChangeStudentGroup(student, newGroup);
            if (!student.NameGroup.ToString().Equals(newGroup.GroupName.ToString()))
            {
                throw new IsuException("Group has not changed");
            }
        }
    }
}