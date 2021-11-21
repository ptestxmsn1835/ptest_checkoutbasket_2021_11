using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class PromotionResult
    {
        public int promotionTotalPrice = 0;
        public List<BasketItem> promotionItems = new();
        public Dictionary<char, List<BasketItem>> nonPromotionItems = new();
    }
}