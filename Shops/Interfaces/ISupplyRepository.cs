using System.Collections.Generic;
using Shops.Classes;

namespace Shops.Interfaces
{
    public interface ISupplyRepository
    {
        Supply RegisterSupply(Supply supply);
        List<Supply> GetAll();
        Supply Find(int id);
        void Print();
    }
}