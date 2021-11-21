using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public static class PromotionType_XofSingleSKU
    {
        public static PromotionResult ApplyPromotionType(List<BasketItem> items, char promotionSKU, int promotionQuantityRequired, int promotionPrice, bool allowMultiplePromotionMatches)
        {
            PromotionResult result = new();
            List<BasketItem> promotionMatchingItems = new();

            foreach (BasketItem item in items)
            {
                if (item.SKU == promotionSKU && (allowMultiplePromotionMatches || result.promotionItems.Count == 0))
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
