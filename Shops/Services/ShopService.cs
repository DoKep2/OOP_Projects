using System;
using System.Collections.Generic;

namespace Shops
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

        public IShopRepository ShopRepository { get; }
        public ProductRepository ProductRepository { get; }
        public ISupplyRepository SupplyRepository { get; }
        public IOrderRepository OrderRepository { get; }

        public Shop CreateShop(string name, int id)
        {
            var newShop = new Shop(name, id);
            ShopRepository.RegisterShop(newShop);
            return newShop;
        }

        public Shop ShopWithCheapestOption(List<Product> products)
        {
            int minimumCost = int.MaxValue;
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

                if (totalCost < minimumCost)
                {
                    minimumCost = Math.Min(minimumCost, totalCost);
                    cheapestShop = currentShop;
                }
            }

            if (cheapestShop == null) throw new ArgumentException("No such shop with such products list");
            return cheapestShop;
        }

        public void AddProducts(List<Product> products, int shopId)
        {
            if (ShopRepository.Find(shopId) == null)
                throw new ArgumentException("Adding products error: no such shop");
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
            if (ShopRepository.Find(shopId) == null) throw new ArgumentException("Purchase error: no such shop");
            int totalCost = 0;
            foreach (Product currentProduct in products)
            {
                Product findingProduct = ProductRepository.Find(currentProduct.ProductId, shopId);
                if (findingProduct == null)
                {
                    throw new ArgumentException(
                        $"Purchase error: shop does not contain such product: {currentProduct.Name}");
                }

                if (findingProduct.Amount < currentProduct.Amount)
                {
                    throw new ArgumentException(
                        $"Purchase error: shop does not contain enough products: {currentProduct.Name}");
                }

                totalCost += currentProduct.Amount * findingProduct.Price;
            }

            if (totalCost > customer.Money)
            {
                throw new ArgumentException(
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