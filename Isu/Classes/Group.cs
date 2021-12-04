using System;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        private const int MAXGROUPNUMBER = 15;
        public Group(int courseNumber, int groupNumber)
        {
            if (!Enum.IsDefined(typeof(CourseNumber), courseNumber))
            {
                throw new IsuException("Invalid course number");
            }

            if (groupNumber > MAXGROUPNUMBER)
            {
                throw new IsuException("Invalid group number");
            }

            CourseNumber = (CourseNumber)courseNumber;
            GroupNumber = groupNumber;
        }

        public CourseNumber CourseNumber { get; }
        public int GroupNumber { get; }

        public override string ToString()
        {
            return "M3" + (int)CourseNumber + $"{GroupNumber:D2}";
        }
    }
}