using NUnit.Framework;
using CheckoutBasketLibrary;
using System;
using System.Collections.Generic;
using CheckoutBasketLibrary.Promotion;

namespace CheckoutBasketLibrary.Tests
{
    class Promotion_MultiplePromotions
    {
        [Test]
        public void CalculateBasketTotal_AllPromotions()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_ThreeA_For_130(),
                new Promotion_TwoB_For_45(),
                new Promotion_OneCOneD_For_30()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 2);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(205, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Specification_ScenarioA()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_ThreeA_For_130(),
                new Promotion_TwoB_For_45(),
                new Promotion_OneCOneD_For_30()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(100, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Specification_ScenarioB()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_ThreeA_For_130(),
                new Promotion_TwoB_For_45(),
                new Promotion_OneCOneD_For_30()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 5);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 5);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(370, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Specification_ScenarioC()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_ThreeA_For_130(),
                new Promotion_TwoB_For_45(),
                new Promotion_OneCOneD_For_30()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 5);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(280, response.totalPrice);
        }
    }
}