using System;
using System.Collections.Generic;

namespace Shops
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _ordersList;

        public OrderRepository()
        {
            _ordersList = new List<Order>();
        }

        public Order RegisterOrder(Order order)
        {
            _ordersList.Add(order);
            return order;
        }

        public List<Order> GetAll()
        {
            return _ordersList;
        }

        public Order Find(int id)
        {
            foreach (Order currentOrder in _ordersList)
            {
                if (currentOrder.OrderId == id)
                {
                    return currentOrder;
                }
            }

            return null;
        }

        public void Print()
        {
            foreach (var currentOrder in _ordersList)
            {
                Console.WriteLine($"Order id: {currentOrder.OrderId}, shop id: {currentOrder.ShopId}, products list:");
                foreach (Product currentProduct in currentOrder.Products)
                {
                    Console.WriteLine(currentProduct);
                }
            }
        }
    }
}