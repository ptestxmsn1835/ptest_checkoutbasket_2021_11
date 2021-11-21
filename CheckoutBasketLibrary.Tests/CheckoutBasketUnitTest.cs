using NUnit.Framework;

namespace CheckoutBasketLibrary.Tests
{
    internal class CheckoutBasketUnitTest
    {
        [Test]
        public void CalculateBasketTotal_EmptyBasket()
        {
            SKUPriceData skuPriceData = new();
            Checkout checkout = new(skuPriceData);
            Basket basket = new();

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(0, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_InvalidSKU()
        {
            SKUPriceData itemPriceData = new();
            Checkout checkout = new(itemPriceData);
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'X', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'Y', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'Z', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.False(response.isSuccessful);
            Assert.AreEqual("Checkout Exception - Invalid SKU(s) - X, Y, Z", response.errorMessage);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_A()
        {
            SKUPriceData itemPriceData = new();
            Checkout checkout = new(itemPriceData);
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(50, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_B()
        {
            SKUPriceData itemPriceData = new();
            Checkout checkout = new(itemPriceData);
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(30, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_C()
        {
            SKUPriceData itemPriceData = new();
            Checkout checkout = new(itemPriceData);
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(20, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_D()
        {
            SKUPriceData itemPriceData = new();
            Checkout checkout = new(itemPriceData);
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(15, response.totalPrice);
        }

        [Test]
        public void CalculateBasketTotal_NoPromotion_SKU_ABCD()
        {
            SKUPriceData itemPriceData = new();
            Checkout checkout = new(itemPriceData);
            Basket basket = new();
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'A', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'B', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'C', 1);
            basket = BasketTestHelper.TestCreateBasketItems(basket, 'D', 1);

            CheckoutResponse response = checkout.CalculateBasketTotal(basket);
            Assert.True(response.isSuccessful);
            Assert.AreEqual(115, response.totalPrice);
        }
    }
}