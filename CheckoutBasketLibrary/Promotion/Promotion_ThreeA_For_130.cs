using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_ThreeA_For_130 : IPromotion
    {
        //3 x A items for 130
        char promotionSKU = 'A';
        readonly int promotionQuantityRequired = 3;
        readonly int promotionPrice = 130;
        bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(List<BasketItem> items)
        {
            return PromotionType_XofSingleSKU.ApplyPromotionType(items, promotionSKU, promotionQuantityRequired, promotionPrice, allowMultiplePromotionMatches);
        }
    }
}
