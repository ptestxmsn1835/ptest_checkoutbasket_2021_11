using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutBasketLibrary.Promotion
{
    public static class PromotionType_XofSingleSKU
    {
        public static PromotionResult ApplyPromotionType(Dictionary<char, List<BasketItem>> items, char promotionSKU, int promotionQuantityRequired, int promotionPrice, bool allowMultiplePromotionMatches)
        {
            PromotionResult result = new()
            {
                nonPromotionItems = items
            };

            //IF required SKU can't be found in basket, return as promotion can't be applied
            if (items.ContainsKey(promotionSKU) && items[promotionSKU].Count >= promotionQuantityRequired)
            {
                List<BasketItem> promotionItems = items[promotionSKU];

                int promotionMatchCount = Convert.ToInt32(Math.Floor((decimal)promotionItems.Count / promotionQuantityRequired));
                if (promotionMatchCount > 1 && !allowMultiplePromotionMatches)
                {
                    promotionMatchCount = 1;
                }

                //For each SKU remove the promotion items from the remaining items to process
                int promotionItemCount = promotionMatchCount * promotionQuantityRequired;
                result.promotionItems.AddRange(promotionItems.Take(promotionItemCount));
                items[promotionSKU] = promotionItems.Skip(promotionItemCount).ToList();

                //Calculate total promotion price
                result.promotionTotalPrice += promotionMatchCount * promotionPrice;
            }

            return result;
        }
    }
}
