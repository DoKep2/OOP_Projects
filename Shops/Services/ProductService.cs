using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace Shops
{
    public class ProductService
    {
        public ProductService(ProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public ProductRepository ProductRepository { get; }
        /*public Product RegisterProduct(string name)
        {
            if (ProductRepository.Contains(name) != null)
            {
                return null;
            }

            Product product = new Product.ProductBuilder()
                .WithName(name)
                .WithProductId(_curId++)
                .Build();
            ProductRepository.RegisterProduct(product);
            return product;
        }*/

        public void UpdatePrice(int productId, int shopId, int newPrice)
        {
            Product previousProduct = ProductRepository.Delete(productId, shopId);
            if (previousProduct == null)
            {
                throw new ArgumentException("Updating product price error: no such product");
            }

            Product newProduct = new Product.ProductBuilder()
                .WithName(previousProduct.Name)
                .WithProductId(productId)
                .WithAmount(previousProduct.Amount)
                .WithPrice(newPrice)
                .WithShopId(shopId)
                .Build();
            ProductRepository.RegisterProduct(newProduct);
        }
    }
}