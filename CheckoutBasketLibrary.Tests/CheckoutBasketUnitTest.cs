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
    }
}