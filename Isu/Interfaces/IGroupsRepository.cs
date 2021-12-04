using System.Collections.Generic;
using Isu.Classes;

namespace Isu.Interfaces
{
    public interface IGroupsRepository
    {
        Group RegisterGroup(Group group);
        List<Group> GetAll();
        void Print();
    }
}