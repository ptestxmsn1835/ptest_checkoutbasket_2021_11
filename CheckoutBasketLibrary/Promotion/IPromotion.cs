using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public interface IPromotion
    {
        PromotionResult ApplyPromotion(Dictionary<char, List<BasketItem>> items);
    }
}
