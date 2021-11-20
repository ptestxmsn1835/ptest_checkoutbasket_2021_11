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
            response.isSuccessful = true;
            return response;
        }
    }

    public class CheckoutResponse
    {
        public bool isSuccessful = false;
        public string errorMessage = "";
        public int totalPrice = 0;
    }
}
