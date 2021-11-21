using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_ThreeA_For_130 : IPromotion
    {
        //3 x A items for 130
        private readonly char promotionSKU = 'A';
        private readonly int promotionQuantityRequired = 3;
        private readonly int promotionPrice = 130;
        private readonly bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(Dictionary<char, List<BasketItem>> items)
        {
            return PromotionType_XofSingleSKU.ApplyPromotionType(items, promotionSKU, promotionQuantityRequired, promotionPrice, allowMultiplePromotionMatches);
        }
    }
}
