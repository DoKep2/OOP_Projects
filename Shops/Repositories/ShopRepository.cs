using System;
using System.Collections.Generic;

namespace Shops
{
    public class ShopRepository : IShopRepository
    {
        private readonly List<Shop> _shopsList;

        public ShopRepository()
        {
            _shopsList = new List<Shop>();
        }

        public Shop RegisterShop(Shop shop)
        {
            _shopsList.Add(shop);
            return shop;
        }

        public List<Shop> GetAll()
        {
            return _shopsList;
        }

        public Shop Find(int id)
        {
            foreach (Shop currentShop in _shopsList)
            {
                if (currentShop.Id == id)
                {
                    return currentShop;
                }
            }

            return null;
        }

        public void Print()
        {
            foreach (Shop currentShop in _shopsList)
            {
                Console.WriteLine($"Shop name: {currentShop.Name}, shop id: {currentShop.Id}");
            }
        }
    }
}