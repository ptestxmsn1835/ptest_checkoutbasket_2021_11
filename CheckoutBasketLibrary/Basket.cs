using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutBasketLibrary
{
    public class Basket
    {
        public List<BasketItem> items = new List<BasketItem>();
    }

    public class BasketItem
    {
        public char SKU { get; set; }
    }
}
