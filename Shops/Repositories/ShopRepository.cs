using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Classes;
using Shops.Interfaces;

namespace Shops.Repositories
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
            return new List<Shop>(_shopsList);
        }

        public Shop Find(int id)
        {
            return _shopsList.FirstOrDefault(currentShop => currentShop.Id == id);
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