using NUnit.Framework;
using CheckoutBasketLibrary;
using System;
using System.Collections.Generic;
using CheckoutBasketLibrary.Promotion;

namespace CheckoutBasketLibrary.Tests
{
    class CheckoutBasketUnitTest
    {
        [Test]
        public void CalculateBasketTotal_EmptyBasket()
        {
            var skuPriceData = new SKUPriceData();
            var checkout = new Checkout(skuPriceData);
            var basket = new Basket();

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(0, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_InvalidSKU()
        {
            var itemPriceData = new SKUPriceData();
            var checkout = new Checkout(itemPriceData);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'X', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'Y', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'Z', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.False(response.isSuccessful);
            Assert.AreEqual("Checkout Exception - Invalid SKU(s) - X, Y, Z", response.errorMessage);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_A()
        {
            var itemPriceData = new SKUPriceData();
            var checkout = new Checkout(itemPriceData);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(50, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_B()
        {
            var itemPriceData = new SKUPriceData();
            var checkout = new Checkout(itemPriceData);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(30, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_C()
        {
            var itemPriceData = new SKUPriceData();
            var checkout = new Checkout(itemPriceData);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(20, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_D()
        {
            var itemPriceData = new SKUPriceData();
            var checkout = new Checkout(itemPriceData);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(15, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_ThreeA_For_130()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 3);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(130, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_Promotion_3xA_for_130_WithRemainingAs()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
                new Promotion_ThreeA_For_130()
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 5);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(230, response.totalPrice);
        }


        [Test]
        public void CalculateBasketTotal_Promotion_2xB_for_45()
        {
            var itemPriceData = new SKUPriceData();
            var promotions = new List<IPromotion>()
            {
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
            };
            var checkout = new Checkout(itemPriceData, promotions);
            var basket = new Basket();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 3);

            var response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(75, response.totalPrice);
        }
    }
}