using System;
using System.Collections.Generic;
using Isu.Classes;
using Isu.Interfaces;

namespace Isu.Repositories
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly List<Group> _groupsList;

        public GroupsRepository()
        {
            _groupsList = new List<Group>();
        }

        public Group RegisterGroup(Group group)
        {
            _groupsList.Add(group);
            return group;
        }

        public List<Group> GetAll()
        {
            return new List<Group>(_groupsList);
        }

        public void Print()
        {
            foreach (Group currentGroup in _groupsList)
            {
                Console.WriteLine(currentGroup);
            }
        }
    }
}