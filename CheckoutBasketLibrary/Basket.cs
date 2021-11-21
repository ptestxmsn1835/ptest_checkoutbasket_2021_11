using System.Collections.Generic;

namespace CheckoutBasketLibrary
{
    public class Basket
    {
        public List<BasketItem> items = new();
    }

    public class BasketItem
    {
        public char SKU { get; set; }
    }
}
