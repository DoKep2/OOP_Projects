using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Classes;
using Shops.Interfaces;

namespace Shops.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _productsList;

        public ProductRepository()
        {
            _productsList = new List<Product>();
        }

        public Product RegisterProduct(Product product)
        {
            _productsList.Add(product);
            return product;
        }

        public List<Product> GetAll()
        {
            return new List<Product>(_productsList);
        }

        public Product Find(int productId, int shopId)
        {
            return _productsList.FirstOrDefault(currentProduct => currentProduct.ProductId == productId
                                                                  && currentProduct.ShopId == shopId);
        }

        public Product Contains(string name)
        {
            return _productsList.FirstOrDefault(currentProduct => currentProduct.Name == name);
        }

        public Product Delete(int productId, int shopId)
        {
            foreach (Product currentProduct in _productsList)
            {
                if (currentProduct.ProductId == productId && currentProduct.ShopId == shopId)
                {
                    _productsList.Remove(currentProduct);
                    return currentProduct;
                }
            }

            return null;
        }

        public void Print()
        {
            foreach (Product currentProduct in _productsList)
            {
                Console.WriteLine(currentProduct);
            }
        }
    }
}