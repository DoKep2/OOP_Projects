namespace Shops.Classes
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