using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Classes;
using Shops.Interfaces;

namespace Shops.Repositories
{
    public class SupplyRepository : ISupplyRepository
    {
        private readonly List<Supply> _suppliesList;

        public SupplyRepository()
        {
            _suppliesList = new List<Supply>();
        }

        public Supply RegisterSupply(Supply supply)
        {
            _suppliesList.Add(supply);
            return supply;
        }

        public List<Supply> GetAll()
        {
            return new List<Supply>(_suppliesList);
        }

        public Supply Find(int id)
        {
            return _suppliesList.FirstOrDefault(currentSupply => currentSupply.SupplyId == id);
        }

        public void Print()
        {
            foreach (Supply currentSupply in _suppliesList)
            {
                Console.WriteLine($"Supply id: {currentSupply.SupplyId}, shop id: {currentSupply.ShopId}, products list:");
                foreach (Product currentProduct in currentSupply.Products)
                {
                    Console.WriteLine(currentProduct);
                }
            }
        }
    }
}