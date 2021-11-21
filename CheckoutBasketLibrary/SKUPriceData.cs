using System.Collections.Generic;

namespace CheckoutBasketLibrary
{
    public class SKUPriceData : ISKUPriceData
    {
        public Dictionary<char, int> GetPriceData()
        {
            return new Dictionary<char, int>()
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 }
            };
        }
    }
}
