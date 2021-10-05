namespace Isu.Services
{
    public class GroupName
    {
        public GroupName(int courseNumber, int groupNumber)
        {
            CourseNumber = new CourseNumber(courseNumber);
            GroupNumber = groupNumber;
        }

        public CourseNumber CourseNumber { get; set; }
        public int GroupNumber { get; set; }

        public override string ToString()
        {
            return "M3" + CourseNumber + string.Format("{0:D2}", GroupNumber);
        }
    }
}