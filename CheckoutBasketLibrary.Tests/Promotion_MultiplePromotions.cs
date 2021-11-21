using CheckoutBasketLibrary.Promotion;
using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutBasketLibrary.Tests
{
    internal class Promotion_MultiplePromotions
    {
        private Checkout checkout;

        [SetUp]
        public void Setup()
        {
            SKUPriceData itemPriceData = new();
            List<IPromotion> promotions = new()
            {
                new Promotion_ThreeA_For_130(),
                new Promotion_TwoB_For_45(),
                new Promotion_OneCOneD_For_30()
            };
            checkout = new Checkout(itemPriceData, promotions);
        }

        [Test]
        public void CalculateBasketTotal_AllPromotions()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 2);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(205, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Specification_ScenarioA()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(100, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Specification_ScenarioB()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 5);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 5);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(370, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Specification_ScenarioC()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 5);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(280, response.totalPrice);
        }
    }
}