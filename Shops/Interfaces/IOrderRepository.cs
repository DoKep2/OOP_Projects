using System.Collections.Generic;
using Shops.Classes;

namespace Shops.Interfaces
{
    public interface IOrderRepository
    {
        Order RegisterOrder(Order order);
        List<Order> GetAll();
        Order Find(int id);
        void Print();
    }
}