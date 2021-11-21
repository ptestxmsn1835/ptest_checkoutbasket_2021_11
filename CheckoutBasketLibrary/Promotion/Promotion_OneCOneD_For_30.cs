using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_OneCOneD_For_30 : IPromotion
    {
        //1 x C and 1 x D  for 30
        List<char> promotionSKUs = new List<char> { 'C', 'D' };
        int promotionPrice = 30;
        bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(List<BasketItem> items)
        {
            return PromotionType_1ofEachSKU.ApplyPromotionType(items, promotionSKUs, promotionPrice, allowMultiplePromotionMatches);
        }
    }
}
