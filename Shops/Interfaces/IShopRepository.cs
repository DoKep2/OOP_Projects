using System.Collections.Generic;
using Shops.Classes;

namespace Shops.Interfaces
{
    public interface IShopRepository
    {
        Shop RegisterShop(Shop shop);
        public List<Shop> GetAll();
        Shop Find(int id);
        void Print();
    }
}