using System.Collections.Immutable;

namespace Shops
{
    public class Shop
    {
        public Shop(string name, int id)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }
        public int Id { get; }
    }
}