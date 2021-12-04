using System.Collections.Generic;
using Isu.Classes;
using Isu.Repositories;
using Isu.Services;

namespace Isu
{
    internal static class Program
    {
        private static void Main()
        {
            GroupsRepository groupsRepository = new GroupsRepository();
            StudentsRepository studentsRepository = new StudentsRepository();
            IsuService isuService = new IsuService(groupsRepository, studentsRepository);
            Group group = isuService.AddGroup(new Group(1, 1));
            isuService.AddStudent(group, "Petya");
            isuService.AddStudent(group, "Vasya");
            studentsRepository.Print();
            List<Student> lst = studentsRepository.GetAll();
            lst.Clear();
            List<Group> gr = groupsRepository.GetAll();
            gr.Clear();
            groupsRepository.Print();
        }
    }
}
