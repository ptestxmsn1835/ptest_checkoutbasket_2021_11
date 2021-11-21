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

            //IF all required SKU's can't be found in basket, return as promotion can't be applied
            if (promotionSKUs.Any(promotionSKU => !groupedBySKU.ContainsKey(promotionSKU)))
            {
                result.nonPromotionItems.AddRange(items);
                return result;
            }
            else
            {
                //Find lowest count of required SKU groupings to apply promotion to
                int maxNumberOfSKUSets = groupedBySKU.Where(groupedSKU => promotionSKUs.Contains(groupedSKU.Key)).Select(g => g.Value.Count).Min();

                //If promotion can only apply once ensure maxNumber is 1 if higher
                if (maxNumberOfSKUSets > 1 && !allowMultiplePromotionMatches)
                {
                    maxNumberOfSKUSets = 1;
                }

                //For each required SKU put remaining items that can't match the promotion into nonPromotionItems for later processing
                foreach (char sku in promotionSKUs)
                {
                    result.promotionItems.AddRange(groupedBySKU[sku].Take(maxNumberOfSKUSets));
                    result.nonPromotionItems.AddRange(groupedBySKU[sku].Skip(maxNumberOfSKUSets));
                }

                //Calculate total promotion price
                result.promotionTotalPrice += maxNumberOfSKUSets * promotionPrice;

                //Move SKU items not matching promotion SKU's into nonPromotionItems for later processing
                foreach (KeyValuePair<char, List<BasketItem>> skuGroup in groupedBySKU.Where(skuGroup => !promotionSKUs.Contains(skuGroup.Key)))
                {
                    result.nonPromotionItems.AddRange(skuGroup.Value);
                }
            }

            return result;
        }
    }
}
