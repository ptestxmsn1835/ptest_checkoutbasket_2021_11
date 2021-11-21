using System.Collections.Generic;
using System.Linq;

namespace CheckoutBasketLibrary.Promotion
{
    public static class PromotionType_1ofEachSKU
    {
        public static PromotionResult ApplyPromotionType(Dictionary<char, List<BasketItem>> items, List<char> promotionSKUs, int promotionPrice, bool allowMultiplePromotionMatches)
        {
            PromotionResult result = new()
            {
                nonPromotionItems = items
            };

            //IF all required SKU's can't be found in basket, return as promotion can't be applied
            if (promotionSKUs.All(promotionSKU => items.ContainsKey(promotionSKU)))
            {
                //Find lowest count of required SKU groupings to apply promotion to
                int maxNumberOfSKUSets = items.Where(groupedSKU => promotionSKUs.Contains(groupedSKU.Key)).Select(g => g.Value.Count).Min();

                //If promotion can only apply once ensure maxNumber is 1 if higher
                if (maxNumberOfSKUSets > 1 && !allowMultiplePromotionMatches)
                {
                    maxNumberOfSKUSets = 1;
                }

                //For each SKU remove the promotion items from the remaining items to process
                foreach (char sku in promotionSKUs)
                {
                    result.promotionItems.AddRange(items[sku].Take(maxNumberOfSKUSets));
                    items[sku] = items[sku].Skip(maxNumberOfSKUSets).ToList();
                }

                //Calculate total promotion price
                result.promotionTotalPrice += maxNumberOfSKUSets * promotionPrice;
            }

            return result;
        }
    }
}
