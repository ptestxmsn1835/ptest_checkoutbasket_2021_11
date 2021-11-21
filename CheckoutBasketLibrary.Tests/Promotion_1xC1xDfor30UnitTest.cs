using NUnit.Framework;
using CheckoutBasketLibrary;
using System;
using System.Collections.Generic;
using CheckoutBasketLibrary.Promotion;

namespace CheckoutBasketLibrary.Tests
{
    class Promotion_1xC1xDfor30UnitTest
    {
        private Checkout checkout;

        [SetUp]
        public void Setup()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_OneCOneD_For_30()
            };
            checkout = new Checkout(itemPriceData, promotions);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_1xC1xD_for_30()
        {
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(30, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_1xC1xD_for_30_WithRemainingCsAndDs()
        {
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 2);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(50, response.totalPrice);

            basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 2);

            response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(45, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_1xC1xD_for_30_WithAB()
        {
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(110, response.totalPrice);
        }
    }
}