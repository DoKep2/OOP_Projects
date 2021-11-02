using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Shops
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
            return _productsList;
        }

        public Product Find(int productId, int shopId)
        {
            foreach (Product currentProduct in _productsList)
            {
                if (currentProduct.ProductId == productId && currentProduct.ShopId == shopId)
                {
                    return currentProduct;
                }
            }

            return null;
        }

        public Product Contains(string name)
        {
            foreach (Product currentProduct in _productsList)
            {
                if (currentProduct.Name == name)
                {
                    return currentProduct;
                }
            }

            return null;
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