using System;

namespace Isu.Services
{
    public class CourseNumber
    {
        private int _courseNumber;

        public CourseNumber(int courseNumber)
        {
            _courseNumber = courseNumber;
        }

        public static bool operator ==(CourseNumber first, CourseNumber second)
        {
            return first._courseNumber == second._courseNumber;
        }

        public static bool operator !=(CourseNumber first, CourseNumber second)
        {
            return first._courseNumber != second._courseNumber;
        }

        public override bool Equals(object obj)
        {
            if (this == (CourseNumber)obj)
            {
                return true;
            }

            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            int num = (int)obj;
            return num.Equals(_courseNumber);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return _courseNumber.ToString();
        }
    }
}