using System.Collections.Generic;

namespace Shops
{
    public interface IShopRepository
    {
        Shop RegisterShop(Shop shop);
        List<Shop> GetAll();
        Shop Find(int id);
        void Print();
    }
}