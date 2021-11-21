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
                //IF item matches required SKU for promotion then check promotion, ELSE move it to nonPromotionItems for later processing
                if (item.SKU == promotionSKU && (allowMultiplePromotionMatches || result.promotionItems.Count == 0))
                {
                    promotionMatchingItems.Add(item);

                    //IF promotion quantity met, update price and clear list for future items
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

            //Move leftover SKU items into nonPromotionItems for later processing
            result.nonPromotionItems.AddRange(promotionMatchingItems);
            return result;
        }
    }
}
