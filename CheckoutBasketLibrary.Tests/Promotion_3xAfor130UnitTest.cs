using CheckoutBasketLibrary.Promotion;
using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutBasketLibrary.Tests
{
    internal class Promotion_3xAfor130UnitTest
    {
        private Checkout checkout;

        [SetUp]
        public void Setup()
        {
            SKUPriceData itemPriceData = new();
            List<IPromotion> promotions = new()
            {
                new Promotion_ThreeA_For_130()
            };
            checkout = new Checkout(itemPriceData, promotions);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(130, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130_WithRemainingAs()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 5);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(230, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130_WithBCD()
        {
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(195, response.totalPrice);
        }
    }
}