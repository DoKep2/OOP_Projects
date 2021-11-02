using System;
using System.Collections.Generic;

namespace Shops
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
            return _suppliesList;
        }

        public Supply Find(int id)
        {
            foreach (Supply currentSupply in _suppliesList)
            {
                if (currentSupply.SupplyId == id)
                {
                    return currentSupply;
                }
            }

            return null;
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