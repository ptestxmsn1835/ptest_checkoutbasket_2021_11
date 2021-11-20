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

        public Checkout(ISKUPriceData skuPriceData)
        {
            skuPrices = skuPriceData.GetPriceData();
        }

        public CheckoutResponse CalculateBasketTotal(Basket basket)
        {
            var response = new CheckoutResponse();
            try
            {
                if (ValidateBasket(basket))
                {
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
