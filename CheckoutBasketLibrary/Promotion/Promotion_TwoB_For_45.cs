using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_TwoB_For_45 : IPromotion
    {
        //2 x B items for 45
        private readonly char promotionSKU = 'B';
        private readonly int promotionQuantityRequired = 2;
        private readonly int promotionPrice = 45;
        private readonly bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(List<BasketItem> items)
        {
            return PromotionType_XofSingleSKU.ApplyPromotionType(items, promotionSKU, promotionQuantityRequired, promotionPrice, allowMultiplePromotionMatches);
        }
    }

}
