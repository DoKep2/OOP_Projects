using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Classes;
using Shops.Interfaces;

namespace Shops.Repositories
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
            return new List<Order>(_ordersList);
        }

        public Order Find(int id)
        {
            return _ordersList.FirstOrDefault(currentOrder => currentOrder.OrderId == id);
        }

        public void Print()
        {
            foreach (Order currentOrder in _ordersList)
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