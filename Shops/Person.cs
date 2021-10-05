using System;

namespace Shops
{
    public class Person
    {
        public Person(string name, int money)
        {
            Name = name;
            Money = money;
        }

        public int Money { get; set; }
        private string Name { get; }
    }
}