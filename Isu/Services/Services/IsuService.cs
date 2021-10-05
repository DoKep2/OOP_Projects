using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int GROUPMAXSIZE = 25;
        private static List<Student> studentById = new List<Student>();
        private static List<Group> groupsList = new List<Group>();

        public List<Group> GetGroupsList()
        {
            return groupsList;
        }

        public Group AddGroup(GroupName name)
        {
            Group newGroup = new Group(name);
            groupsList.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            Student newStudent = new Student(name);
            newStudent.NameGroup = group.GroupName;
            foreach (Group curGroup in groupsList)
            {
                if (curGroup.GroupName.ToString().Equals(group.GroupName.ToString()))
                {
                    if (curGroup.StudentList.Count + 1 > GROUPMAXSIZE)
                    {
                        throw new IsuException("Group is full");
                    }

                    curGroup.StudentList.Add(newStudent);
                }
            }

            studentById.Add(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            return studentById[id];
        }

        public Student FindStudent(string name)
        {
            foreach (Student curStudent in studentById)
            {
                if (curStudent.Name == name)
                {
                    return curStudent;
                }
            }

            /*Console.ForegroundColor = ConsoleColor.Red;*/
            throw new IsuException("Student not found");
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            foreach (var curGroup in groupsList)
            {
                if (curGroup.GroupName.ToString().Equals(groupName.ToString()))
                {
                    return curGroup.StudentList;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            throw new IsuException("Group not found");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> curStudentList = new List<Student>();
            foreach (Student curStudent in studentById)
            {
                if (curStudent.NameGroup.CourseNumber == courseNumber)
                {
                    curStudentList.Add(curStudent);
                }
            }

            if (curStudentList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Students list is empty");
                Console.ResetColor();
            }

            return curStudentList;
        }

        public Group FindGroup(GroupName groupName)
        {
            foreach (Group curGroup in groupsList)
            {
                if (curGroup.GroupName.ToString().Equals(groupName.ToString()))
                {
                    return curGroup;
                }
            }

            throw new IsuException("Group not found");
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> curGroupsList = new List<Group>();
            foreach (var curGroup in groupsList)
            {
                if (curGroup.GroupName.CourseNumber == courseNumber)
                {
                    curGroupsList.Add(curGroup);
                }
            }

            if (curGroupsList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Groups list is empty");
                Console.ResetColor();
            }

            return curGroupsList;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.StudentList.Count == GROUPMAXSIZE)
            {
                throw new IsuException("Group is full");
            }

            foreach (Student curStudent in studentById)
            {
                if (curStudent.Id == student.Id)
                {
                    curStudent.NameGroup = newGroup.GroupName;
                }
            }

            foreach (Group curGroup in groupsList)
            {
                if (curGroup.GroupName == student.NameGroup)
                {
                    curGroup.StudentList.Remove(student);
                }
                else if (curGroup.GroupName == newGroup.GroupName)
                {
                    curGroup.StudentList.Add(student);
                }
            }
        }
    }
}