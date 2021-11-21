using NUnit.Framework;
using CheckoutBasketLibrary;
using System;
using System.Collections.Generic;
using CheckoutBasketLibrary.Promotion;

namespace CheckoutBasketLibrary.Tests
{
    class Promotion_3xAfor130UnitTest
    {
        private Checkout checkout;

        [SetUp]
        public void Setup()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_ThreeA_For_130()
            };
            checkout = new Checkout(itemPriceData, promotions);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130()
        {
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(130, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130_WithRemainingAs()
        {
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 5);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(230, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130_WithBCD()
        {
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(195, response.totalPrice);
        }
    }
}