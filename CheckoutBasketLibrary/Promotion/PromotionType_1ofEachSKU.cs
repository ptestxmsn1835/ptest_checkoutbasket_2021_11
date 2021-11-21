using System.Collections.Generic;
using System.Linq;

namespace CheckoutBasketLibrary.Promotion
{
    public static class PromotionType_1ofEachSKU
    {
        public static PromotionResult ApplyPromotionType(List<BasketItem> items, List<char> promotionSKUs, int promotionPrice, bool allowMultiplePromotionMatches)
        {
            PromotionResult result = new();

            Dictionary<char, List<BasketItem>> groupedBySKU = items.GroupBy(i => i.SKU).ToDictionary(i => i.Key, i => i.ToList());

            if (promotionSKUs.Any(promotionSKU => !groupedBySKU.ContainsKey(promotionSKU)))
            {
                result.nonPromotionItems.AddRange(items);
                return result;
            }
            else
            {
                int maxNumberOfSKUSets = groupedBySKU.Where(groupedSKU => promotionSKUs.Contains(groupedSKU.Key)).Select(g => g.Value.Count).Min();

                if (maxNumberOfSKUSets > 1 && !allowMultiplePromotionMatches)
                {
                    maxNumberOfSKUSets = 1;
                }

                foreach (char sku in promotionSKUs)
                {
                    result.promotionItems.AddRange(groupedBySKU[sku].Take(maxNumberOfSKUSets));
                    result.nonPromotionItems.AddRange(groupedBySKU[sku].Skip(maxNumberOfSKUSets));
                }

                result.promotionTotalPrice += maxNumberOfSKUSets * promotionPrice;

                foreach (KeyValuePair<char, List<BasketItem>> skuGroup in groupedBySKU.Where(skuGroup => !promotionSKUs.Contains(skuGroup.Key)))
                {
                    result.nonPromotionItems.AddRange(skuGroup.Value);
                }
            }

            return result;
        }
    }
}
