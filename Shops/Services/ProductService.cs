using Shops.Classes;
using Shops.Exceptions;
using Shops.Repositories;

namespace Shops.Services
{
    public class ProductService
    {
        public ProductService(ProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        private ProductRepository ProductRepository { get; }
        public void UpdatePrice(int productId, int shopId, int newPrice)
        {
            Product previousProduct = ProductRepository.Delete(productId, shopId);
            if (previousProduct == null)
            {
                throw new ProductException("Updating product price error: no such product");
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