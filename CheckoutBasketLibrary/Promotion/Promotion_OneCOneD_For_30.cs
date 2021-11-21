using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_OneCOneD_For_30 : IPromotion
    {
        //1 x C and 1 x D  for 30
        private readonly List<char> promotionSKUs = new() { 'C', 'D' };
        private readonly int promotionPrice = 30;
        private readonly bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(Dictionary<char, List<BasketItem>> items)
        {
            return PromotionType_1ofEachSKU.ApplyPromotionType(items, promotionSKUs, promotionPrice, allowMultiplePromotionMatches);
        }
    }
}
