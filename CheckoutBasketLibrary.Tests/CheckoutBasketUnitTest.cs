using NUnit.Framework;
using CheckoutBasketLibrary;
using System;
using System.Collections.Generic;

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
    }
}