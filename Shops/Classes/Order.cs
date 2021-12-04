using System.Collections.Generic;

namespace Shops.Classes
{
    public class Order
    {
        private Order(List<Product> products, int orderId, int shopId)
        {
            Products = products;
            OrderId = orderId;
            ShopId = shopId;
        }

        public List<Product> Products { get; }
        public int OrderId { get; }
        public int ShopId { get; }
        public OrderBuilder ToBuilder()
        {
            return new OrderBuilder()
                .WithShopId(ShopId)
                .WithProducts(Products)
                .WithOrderId(OrderId);
        }

        public class OrderBuilder
        {
            private List<Product> _products;
            private int _shopId;
            private int _orderId;

            public Order Build()
            {
                return new Order(_products, _orderId, _shopId);
            }

            public OrderBuilder WithProducts(List<Product> products)
            {
                _products = products;
                return this;
            }

            public OrderBuilder WithShopId(int shopId)
            {
                _shopId = shopId;
                return this;
            }

            public OrderBuilder WithOrderId(int orderId)
            {
                _orderId = orderId;
                return this;
            }
        }
    }
}