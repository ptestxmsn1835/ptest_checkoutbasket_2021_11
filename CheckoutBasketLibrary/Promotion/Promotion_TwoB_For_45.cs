using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_TwoB_For_45 : IPromotion
    {
        //2 x B items for 45
        char promotionSKU = 'B';
        readonly int promotionQuantityRequired = 2;
        readonly int promotionPrice = 45;
        bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(List<BasketItem> items)
        {
            return PromotionType_XofSingleSKU.ApplyPromotionType(items, promotionSKU, promotionQuantityRequired, promotionPrice, allowMultiplePromotionMatches);
        }
    }

}
