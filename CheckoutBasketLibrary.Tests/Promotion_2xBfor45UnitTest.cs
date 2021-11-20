using NUnit.Framework;
using CheckoutBasketLibrary;
using System;
using System.Collections.Generic;
using CheckoutBasketLibrary.Promotion;

namespace CheckoutBasketLibrary.Tests
{
    class Promotion_2xBfor45UnitTest
    {
        [Test]
        public void CalculateBasketTotal_Promotion_2xB_for_45()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_TwoB_For_45()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 2);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(45, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_2xB_for_45_WithRemainingBs()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            { 
                new Promotion_TwoB_For_45()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 3);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(75, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_2xB_for_45_WithACD()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_TwoB_For_45()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 2);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(130, response.totalPrice);
        }
    }
}