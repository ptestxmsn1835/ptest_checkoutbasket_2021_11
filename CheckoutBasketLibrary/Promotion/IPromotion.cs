using System.Collections.Generic;

namespace CheckoutBasketLibrary.Promotion
{
    public interface IPromotion
    {
        PromotionResult ApplyPromotion(List<BasketItem> items);
    }
}
