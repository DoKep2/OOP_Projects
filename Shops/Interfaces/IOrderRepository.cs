using System.Collections.Generic;

namespace Shops
{
    public interface IOrderRepository
    {
        Order RegisterOrder(Order order);
        List<Order> GetAll();
        Order Find(int id);
        void Print();
    }
}