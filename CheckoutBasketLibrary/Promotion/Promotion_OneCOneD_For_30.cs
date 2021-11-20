using System.Collections.Generic;
using System.Linq;

namespace CheckoutBasketLibrary.Promotion
{
    public class Promotion_OneCOneD_For_30 : IPromotion
    {
        //1 x C and 1 x D  for 30
        int promotionPrice = 30;
        bool allowMultiplePromotionMatches = true;

        public PromotionResult ApplyPromotion(List<BasketItem> items)
        {
            var result = new PromotionResult();
            var potentialItemsC = new List<BasketItem>();
            var potentialItemsD = new List<BasketItem>();
         
            foreach (var item in items)
            {
                switch (item.SKU)
                {
                    case 'C':
                        potentialItemsC.Add(item);
                        break;
                    case 'D':
                        potentialItemsD.Add(item);
                        break;
                    default:
                        result.nonPromotionItems.Add(item);
                        break;
                }
            }

            if (potentialItemsC.Count > 0 && potentialItemsD.Count > 0)
            {
                int maxCountOfCDPairs = potentialItemsC.Count <= potentialItemsD.Count ? potentialItemsC.Count : potentialItemsD.Count;

                if (maxCountOfCDPairs > 1 && !allowMultiplePromotionMatches)
                    maxCountOfCDPairs = 1;

                result.promotionItems.AddRange(potentialItemsC.Take(maxCountOfCDPairs));
                result.promotionItems.AddRange(potentialItemsD.Take(maxCountOfCDPairs));

                result.promotionTotalPrice += maxCountOfCDPairs * promotionPrice;

                result.nonPromotionItems.AddRange(potentialItemsC.Skip(maxCountOfCDPairs));
                result.nonPromotionItems.AddRange(potentialItemsD.Skip(maxCountOfCDPairs));
            }
            else
            {
                result.nonPromotionItems.AddRange(potentialItemsC);
                result.nonPromotionItems.AddRange(potentialItemsD);
            }

            return result;
        }
    }
}
