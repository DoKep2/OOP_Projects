using System.Collections.Generic;

namespace Shops
{
    public interface IProductRepository
    {
        Product RegisterProduct(Product product);
        List<Product> GetAll();
        Product Find(int productId, int shopId);
        void Print();
    }
}