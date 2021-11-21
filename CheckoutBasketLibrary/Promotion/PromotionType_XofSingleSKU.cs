using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutBasketLibrary.Promotion
{
    public static class PromotionType_XofSingleSKU
    {
        public static PromotionResult ApplyPromotionType(List<BasketItem> items, char promotionSKU, int promotionQuantityRequired, int promotionPrice, bool allowMultiplePromotionMatches)
        {
            var result = new PromotionResult();
            var promotionMatchingItems = new List<BasketItem>();

            foreach (var item in items)
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
