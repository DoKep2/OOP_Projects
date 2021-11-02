using System.Collections.Generic;

namespace Shops
{
    public interface ISupplyRepository
    {
        Supply RegisterSupply(Supply supply);
        List<Supply> GetAll();
        Supply Find(int id);
        void Print();
    }
}