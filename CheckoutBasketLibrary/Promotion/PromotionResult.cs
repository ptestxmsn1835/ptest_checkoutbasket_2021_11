using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutBasketLibrary.Promotion
{
    public class PromotionResult
    {
        public int promotionTotalPrice = 0;
        public List<BasketItem> promotionItems = new List<BasketItem>();
        public List<BasketItem> nonPromotionItems = new List<BasketItem>();
    }
}
