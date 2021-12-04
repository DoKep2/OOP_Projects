using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Classes;
using Shops.Exceptions;
using Shops.Repositories;
using Shops.Services;

namespace Shops.Tests
{
    [TestFixture]
    public class ShopsTest
    {
        
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        private SupplyRepository _supplyRepository;
        private ShopRepository _shopRepository;
        private ShopService _shopService;
        private ProductService _productService;
        [SetUp]
        public void Setup()
        {
            _orderRepository = new OrderRepository();
            _productRepository = new ProductRepository();
            _supplyRepository = new SupplyRepository();
            _shopRepository = new ShopRepository();
            _shopService = new ShopService(_shopRepository, _productRepository, _supplyRepository, _orderRepository);
            _productService = new ProductService(_productRepository);
        }
        [TestCase("Shop1", 1)]
        public void CreateShop_ShopWasAddedToRepository(string name, int id)
        {
            Shop shop = _shopService.CreateShop("Shop1", 1);
            CollectionAssert.Contains(_shopRepository.GetAll()
                .Where(currentShop => currentShop.Name.Equals(name) && currentShop.Id == id), shop);
        }

        private static readonly object[] ProductsList =
        {
            new object[]
            {
                new List<Product>
                {
                    new Product.ProductBuilder()
                        .WithName("product1")
                        .WithAmount(5)
                        .WithPrice(50)
                        .WithProductId(1)
                        .WithShopId(1)
                        .Build()
                }
            }
        };

        private static readonly object[] Customer =
        {
            new object[]
            {
                new Customer("Petya", 500)
            }
        };
        [TestCaseSource(nameof(ProductsList))]
        public void FindTheCheapestShopInEmptyRepository_ThrowException(List<Product> products)
        {
            Assert.Catch<ShopException>(() =>
            {
                var shopRepository = new ShopRepository();
                _shopService.ShopWithCheapestOption(products);
            });
        }

        [TestCaseSource(nameof(ProductsList))]
        public void PurchaseToInvalidShop_ThrowException(List<Product> products)
        {
            Assert.Catch<ShopException>(() =>
            {
                _shopRepository = new ShopRepository();
                var customer = new Customer("Petya", 500);
                _shopService.Purchase(ref customer, products, -1);
            });
        }

        [Test]
        public void UpdatePriceOfInvalidProduct_ThrowException()
        {
            Assert.Catch<ProductException>(() =>
            {
                _productService.UpdatePrice(-1, -1, -1);
            });
        }
        [TestCaseSource(nameof(ProductsList))]
        public void UpdateProductPrice_PriceUpdated(List<Product> products)
        {
            _shopService.CreateShop("shop1", 1);
            const int newPrice = 100;
            _shopService.AddProducts(products, 1);
            _productService.UpdatePrice(1, 1, newPrice);
            foreach (Product currentProduct in _productRepository.GetAll()
                .Where(currentProduct => currentProduct.ProductId == 1 && currentProduct.ShopId == 1))
            {
                Assert.AreEqual(currentProduct.Price, newPrice);
            }
        }
    }
}