using System.Collections.Generic;
using Shops.Classes;

namespace Shops.Interfaces
{
    public interface IProductRepository
    {
        Product RegisterProduct(Product product);
        List<Product> GetAll();
        Product Find(int productId, int shopId);
        void Print();
    }
}