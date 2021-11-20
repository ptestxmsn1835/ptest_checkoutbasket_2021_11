using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_TwoB_For_45 : IPromotion
    {
        //2 x B items for 45
        char promotionItemType = 'B';
        readonly int promotionQuantityRequired = 2;
        readonly int promotionPrice = 45;
        bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(List<BasketItem> items)
        {
            var result = new PromotionResult();
            var promotionMatchingItems = new List<BasketItem>();

            foreach (var item in items)
            {
                if (item.SKU == promotionItemType && (allowMultiplePromotionMatches || result.promotionItems.Count == 0))
                {
                    promotionMatchingItems.Add(item);

                    if (promotionMatchingItems.Count == promotionQuantityRequired)
                    {
                        result.promotionItems.AddRange(promotionMatchingItems);
                        result.promotionTotalPrice += promotionPrice;
                        promotionMatchingItems.Clear();
                    }
                }
                else
                {
                    result.nonPromotionItems.Add(item);
                }
            }

            result.nonPromotionItems.AddRange(promotionMatchingItems);

            return result;
        }
    }

}
