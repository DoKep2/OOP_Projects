namespace Shops
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public override bool Equals(object obj)
        {
            return obj != null && Name.Equals(((Product)obj).Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}