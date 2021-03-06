namespace CheckoutBasketLibrary.Tests
{
    public static class BasketTestHelper
    {
        public static Basket TestCreateBasketItems(Basket basket, char sku, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                basket.items.Add(new BasketItem() { SKU = sku });
            }

            return basket;
        }
    }
}
