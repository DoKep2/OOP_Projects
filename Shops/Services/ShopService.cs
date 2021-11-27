using System;
using System.Collections.Generic;
using Shops.Classes;
using Shops.Exceptions;
using Shops.Interfaces;
using Shops.Repositories;

namespace Shops.Services
{
    public class ShopService
    {
        private int _supplyId;
        private int _orderId;

        public ShopService(
            IShopRepository shopRepository,
            ProductRepository productRepository,
            ISupplyRepository supplyRepository,
            IOrderRepository orderRepository)
        {
            ShopRepository = shopRepository;
            ProductRepository = productRepository;
            SupplyRepository = supplyRepository;
            OrderRepository = orderRepository;
        }

        private IShopRepository ShopRepository { get; }
        private ProductRepository ProductRepository { get; }
        private ISupplyRepository SupplyRepository { get; }
        private IOrderRepository OrderRepository { get; }

        public Shop CreateShop(string name, int id)
        {
            var newShop = new Shop(name, id);
            ShopRepository.RegisterShop(newShop);
            return newShop;
        }

        public Shop ShopWithCheapestOption(List<Product> products)
        {
            int? minimumCost = null;
            Shop cheapestShop = null;
            foreach (Shop currentShop in ShopRepository.GetAll())
            {
                int shopId = currentShop.Id;
                int totalCost = 0;
                foreach (Product curProduct in products)
                {
                    int productId = curProduct.ProductId;
                    Product findingProduct = ProductRepository.Find(productId, shopId);
                    if (findingProduct == null || findingProduct.Amount < curProduct.Amount)
                    {
                        totalCost = int.MaxValue;
                        break;
                    }

                    totalCost += findingProduct.Price;
                }

                if (minimumCost == null || totalCost <= minimumCost)
                {
                    minimumCost = totalCost;
                    cheapestShop = currentShop;
                }
            }

            if (cheapestShop == null)
            {
                throw new ShopException("Finding the cheapest shop error: No such shop with such products list");
            }

            return cheapestShop;
        }

        public void AddProducts(List<Product> products, int shopId)
        {
            if (ShopRepository.Find(shopId) == null)
                throw new ShopException("Adding products error: no such shop");
            foreach (Product addedProduct in products)
            {
                Product findingProduct = ProductRepository.Find(addedProduct.ProductId, addedProduct.ShopId);
                ProductRepository.RegisterProduct(new Product.ProductBuilder()
                    .WithName(addedProduct.Name)
                    .WithProductId(addedProduct.ProductId)
                    .WithAmount((findingProduct?.Amount ?? 0) + addedProduct.Amount)
                    .WithPrice(addedProduct.Price)
                    .WithShopId(shopId)
                    .Build());
            }

            SupplyRepository.RegisterSupply(new Supply.SupplyBuilder()
                .WithShopId(shopId)
                .WithProducts(products)
                .WithSupplyId(_supplyId++)
                .Build());
        }

        public void Purchase(ref Customer customer, List<Product> products, int shopId)
        {
            if (ShopRepository.Find(shopId) == null) throw new ShopException("Purchase error: no such shop");
            int totalCost = 0;
            foreach (Product currentProduct in products)
            {
                Product findingProduct = ProductRepository.Find(currentProduct.ProductId, shopId);
                if (findingProduct == null)
                {
                    throw new ShopException(
                        $"Purchase error: shop does not contain such product: {currentProduct.Name}");
                }

                if (findingProduct.Amount < currentProduct.Amount)
                {
                    throw new ShopException(
                        $"Purchase error: shop does not contain enough products: {currentProduct.Name}");
                }

                totalCost += currentProduct.Amount * findingProduct.Price;
            }

            if (totalCost > customer.Money)
            {
                throw new ShopException(
                    $"Purchase error: customer {customer.Name} does not have enough money");
            }

            foreach (Product currentProduct in products)
            {
                Product findingProduct = ProductRepository.Delete(currentProduct.ProductId, shopId);
                ProductRepository.RegisterProduct(new Product.ProductBuilder()
                    .WithName(currentProduct.Name)
                    .WithProductId(currentProduct.ProductId)
                    .WithAmount(findingProduct.Amount - currentProduct.Amount)
                    .WithPrice(findingProduct.Price)
                    .WithShopId(shopId)
                    .Build());
                customer = new Customer(customer.Name, customer.Money - totalCost);
            }

            OrderRepository.RegisterOrder(new Order.OrderBuilder()
                .WithShopId(shopId)
                .WithProducts(products)
                .WithOrderId(_orderId++)
                .Build());
        }
    }
}