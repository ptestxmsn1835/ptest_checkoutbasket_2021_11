using CheckoutBasketLibrary.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutBasketLibrary
{
    public class Checkout
    {
        private readonly Dictionary<char, int> skuPrices = new Dictionary<char, int>();
        private readonly List<IPromotion> promotions = new List<IPromotion>();

        public Checkout(ISKUPriceData skuPriceData, List<IPromotion> promotions = null)
        {
            skuPrices = skuPriceData.GetPriceData();
            this.promotions = promotions ?? new List<IPromotion>();
        }

        public CheckoutResponse CalculateBasketTotal(Basket basket)
        {
            var response = new CheckoutResponse();
            try
            {
                if (ValidateBasket(basket))
                {
                    var remainingBasketItems = basket.items;

                    foreach (var promotion in promotions)
                    {
                        var promotionResponse = promotion.ApplyPromotion(remainingBasketItems);
                        response.totalPrice += promotionResponse.promotionTotalPrice;
                        remainingBasketItems = promotionResponse.nonPromotionItems;
                    }

                    foreach (var item in remainingBasketItems)
                    {
                        response.totalPrice += skuPrices[item.SKU];
                    }

                    response.isSuccessful = true;
                }
            }
            catch(KeyNotFoundException e)
            {
                response.errorMessage = e.Message;
            }
            catch(Exception e)
            {
                response.errorMessage = e.Message;
            }

            return response;
        }

        private bool ValidateBasket(Basket basket)
        {
            if (basket.items.Count == 0)
                return true;

            var distinctInvalidSKUs = basket.items.Select(i => i.SKU).Where(sku => !skuPrices.ContainsKey(sku)).Distinct().OrderBy(sku => sku).ToList();

            if(distinctInvalidSKUs.Count > 0)
            {
                throw new KeyNotFoundException($"Checkout Exception - Invalid SKU(s) - {string.Join(", ", distinctInvalidSKUs)}");
            }

            return true;
        }
    }

    public class CheckoutResponse
    {
        public bool isSuccessful = false;
        public string errorMessage = "";
        public int totalPrice = 0;
    }
}
