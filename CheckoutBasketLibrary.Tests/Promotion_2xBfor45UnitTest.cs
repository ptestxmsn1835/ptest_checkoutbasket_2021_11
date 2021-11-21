using CheckoutBasketLibrary.Promotion;
using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutBasketLibrary.Tests
{
    internal class Promotion_2xBfor45UnitTest
    {
        private Checkout checkout;

        [SetUp]
        public void Setup()
        {
            SKUPriceData itemPriceData = new();
            List<IPromotion> promotions = new()
            {
                new Promotion_TwoB_For_45()
            };
            checkout = new Checkout(itemPriceData, promotions);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_2xB_for_45()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 2);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(45, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_2xB_for_45_WithRemainingBs()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 3);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(75, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_2xB_for_45_WithACD()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 2);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(130, response.totalPrice);
        }
    }
}