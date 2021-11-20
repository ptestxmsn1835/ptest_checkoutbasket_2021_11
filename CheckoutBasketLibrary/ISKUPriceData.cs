using System.Collections.Generic;

namespace CheckoutBasketLibrary
{
    public interface ISKUPriceData
    {
        Dictionary<char, int> GetPriceData();
    }
}