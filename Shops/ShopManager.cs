using System;
using System.Collections.Generic;

namespace Shops
{
    public class ShopManager
    {
        private static List<Shop> shopsList = new List<Shop>();
        public Shop Create(string shopName, string address, int id)
        {
            var newShop = new Shop(shopName, address, id);
            shopsList.Add(newShop);
            return newShop;
        }

        public Shop FindTheCheapestOption(List<(Product product, int amount)> products)
        {
            int minPrice = int.MaxValue;
            Shop cheapestShop = null;
            foreach (Shop curShop in shopsList)
            {
                int totalPrice = -1;
                foreach ((Product product, int amount) in products)
                {
                    string productName = product.Name;
                    if (!curShop.Products.ContainsKey(productName) || curShop.Products[productName].amount < amount)
                    {
                        totalPrice = -1;
                        break;
                    }

                    totalPrice += curShop.Products[productName].price * amount;
                }

                if (totalPrice != -1)
                {
                    if (minPrice > totalPrice)
                    {
                        minPrice = totalPrice;
                        cheapestShop = curShop;
                    }
                }
            }

            if (cheapestShop == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("No satisfying shops");
            }

            return cheapestShop;
        }
    }
}