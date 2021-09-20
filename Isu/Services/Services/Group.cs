using System.Collections.Generic;
using System.Net.Http.Headers;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        public Group(GroupName groupName)
        {
            GroupName = groupName;
            StudentList = new List<Student>();
        }

        public Group(int courseNumber, int groupNumber)
        {
            if (courseNumber >= 10 || courseNumber <= 0)
            {
                throw new IsuException("Invalid course number");
            }

            if (groupNumber >= 100 || groupNumber <= 0)
            {
                throw new IsuException("Invalid group number");
            }

            GroupName = new GroupName(courseNumber, groupNumber);
            StudentList = new List<Student>();
        }

        public List<Student> StudentList { get; }
        public GroupName GroupName { get; }
    }
}