using System.Collections.Generic;

namespace Shops.Classes
{
    public class Supply
    {
        public Supply(List<Product> products, int shopId, int supplyId)
        {
            Products = products;
            ShopId = shopId;
            SupplyId = supplyId;
        }

        public List<Product> Products { get; }
        public int ShopId { get; }
        public int SupplyId { get; }

        public SupplyBuilder ToBuilder()
        {
            return new SupplyBuilder()
                .WithShopId(ShopId)
                .WithProducts(Products)
                .WithSupplyId(SupplyId);
        }

        public class SupplyBuilder
        {
            private List<Product> _products;
            private int _shopId;
            private int _supplyId;

            public Supply Build()
            {
                return new Supply(_products, _shopId, _supplyId);
            }

            public SupplyBuilder WithProducts(List<Product> products)
            {
                _products = products;
                return this;
            }

            public SupplyBuilder WithShopId(int shopId)
            {
                _shopId = shopId;
                return this;
            }

            public SupplyBuilder WithSupplyId(int supplyId)
            {
                _supplyId = supplyId;
                return this;
            }
        }
    }
}