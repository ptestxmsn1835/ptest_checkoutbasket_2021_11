using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_ThreeA_For_130 : IPromotion
    {
        //3 x A items for 130
        char promotionItemType = 'A';
        readonly int promotionQuantityRequired = 3;
        readonly int promotionPrice = 130;
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
