namespace Shops.Classes
{
    public class Product
    {
        private Product(int price, int amount, int shopId, string name, int productId)
        {
            Price = price;
            Amount = amount;
            ShopId = shopId;
            Name = name;
            ProductId = productId;
        }

        public int Price { get; }
        public int Amount { get; }
        public int ShopId { get; }
        public string Name { get; }
        public int ProductId { get; }

        public override string ToString()
        {
            return $"Price: {Price}, Amount: {Amount}, ShopId: {ShopId}, Name: {Name}, Id: {ProductId}";
        }

        public ProductBuilder ToBuilder()
        {
            return new ProductBuilder()
                .WithAmount(Amount)
                .WithName(Name)
                .WithPrice(Price)
                .WithProductId(ProductId)
                .WithShopId(ShopId);
        }

        public class ProductBuilder
        {
            private int _price;
            private int _amount;
            private int _shopId;
            private string _name;
            private int _productId;

            public Product Build()
            {
                return new Product(
                    _price,
                    _amount,
                    _shopId,
                    _name,
                    _productId);
            }

            public ProductBuilder WithPrice(int price)
            {
                _price = price;
                return this;
            }

            public ProductBuilder WithAmount(int amount)
            {
                _amount = amount;
                return this;
            }

            public ProductBuilder WithShopId(int shopId)
            {
                _shopId = shopId;
                return this;
            }

            public ProductBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public ProductBuilder WithProductId(int productId)
            {
                _productId = productId;
                return this;
            }
        }
    }
}