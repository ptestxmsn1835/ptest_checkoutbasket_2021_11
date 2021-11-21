﻿using CheckoutBasketLibrary.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutBasketLibrary
{
    public class Checkout
    {
        private readonly Dictionary<char, int> skuPrices = new();
        private readonly List<IPromotion> promotions = new();

        //Dependency Inversion allows different pricing models and promotion lists to be used
        public Checkout(ISKUPriceData skuPriceData, List<IPromotion> promotions = null)
        {
            skuPrices = skuPriceData.GetPriceData();
            this.promotions = promotions ?? new List<IPromotion>();
        }

        public CheckoutResponse CalculateBasketTotal(Basket basket)
        {
            CheckoutResponse response = new();
            try
            {
                //Validate Basket to ensure bad input is stopped here
                ValidateBasket(basket);
                List<BasketItem> remainingBasketItems = basket.items;

                //Fall-Through Pattern - Each promotion removes items that met promotion while allowing remaining items to be considered for future promotions
                foreach (IPromotion promotion in promotions)
                {
                    PromotionResult promotionResponse = promotion.ApplyPromotion(remainingBasketItems);
                    response.totalPrice += promotionResponse.promotionTotalPrice;
                    remainingBasketItems = promotionResponse.nonPromotionItems;
                }

                //Remaining items that met no promotion criteria are calculated here
                foreach (BasketItem item in remainingBasketItems)
                {
                    response.totalPrice += skuPrices[item.SKU];
                }

                response.isSuccessful = true;
            }
            catch (Exception e)
            {
                response.errorMessage = e.Message;
            }

            return response;
        }

        private void ValidateBasket(Basket basket)
        {
            List<char> distinctInvalidSKUs = basket.items.Select(i => i.SKU).Where(sku => !skuPrices.ContainsKey(sku)).Distinct().OrderBy(sku => sku).ToList();

            if (distinctInvalidSKUs.Count > 0)
            {
                throw new KeyNotFoundException($"Checkout Exception - Invalid SKU(s) - {string.Join(", ", distinctInvalidSKUs)}");
            }
        }
    }

    public class CheckoutResponse
    {
        public bool isSuccessful = false;
        public string errorMessage = "";
        public int totalPrice = 0;
    }
}
