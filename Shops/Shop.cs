using System;
using System.Collections.Generic;

namespace Shops
{
    public class Shop
    {
        public Shop(string name, string address, int id)
        {
            Name = name;
            Address = address;
            Id = id;
            RegProducts = new List<Product>();
            Products = new Dictionary<string, (int price, int amount)>();
        }

        public Dictionary<string, (int price, int amount)> Products { get; }
        private List<Product> RegProducts { get; }
        private int Id { get; }
        private string Name { get; }
        private string Address { get; }

        public Product RegisterProduct(string productName)
        {
            Product curProduct = new Product(productName);
            if (RegProducts.Contains(curProduct))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("Product is already registered");
            }

            RegProducts.Add(curProduct);
            return curProduct;
        }

        public (int price, int amount) GetProductInfo(Product product)
        {
            return Products[product.Name];
        }

        public void AddProducts(List<(Product product, int price, int amount)> productsList)
        {
            foreach ((Product product, int price, int amount) in productsList)
            {
                if (!RegProducts.Contains(product))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new ArgumentException("No such registered product");
                }

                if (!Products.ContainsKey(product.Name))
                {
                    Products[product.Name] = (price, amount);
                }
                else
                {
                    int previousAmount = Products[product.Name].amount;
                    Products[product.Name] = (price, previousAmount + amount);
                }
            }
        }

        public void Buy(Person person, List<(Product product, int amount)> products)
        {
            foreach ((Product product, int amount) in products)
            {
                int price = Products[product.Name].price;
                int availableAmount = Products[product.Name].amount;
                if (amount > availableAmount)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new ArgumentException("Shop haven't enough products");
                }

                if (price * amount > person.Money)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new ArgumentException("Person haven't enough money");
                }

                person.Money -= price * amount;
                Products[product.Name] = (price, availableAmount - amount);
            }
        }

        public void ChangePrice(Product product, int newPrice)
        {
            if (!Products.ContainsKey(product.Name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("Unknown product");
            }

            Products[product.Name] = (newPrice, Products[product.Name].amount);
        }

        public override string ToString()
        {
            return $"Name: {Name}, address: {Address}, id: {Id}";
        }
    }
}