namespace StorefrontAPI
{
    public static class MemoryDatabase
    {
        //store orders in memory for now
        //For debugging purposes, add a single order to the list. We will use the Walnuts from the list of products.
        public static List<NetworkResources.Order> Orders = new List<NetworkResources.Order>
        {
            new NetworkResources.Order
            {
                Id = 0,
                Date = System.DateTime.Now,
                TotalPrice = 60.00m,
                OrderItems = new List<NetworkResources.OrderItem>
                {
                    new NetworkResources.OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1,
                        Price = 60.00m,
                        Name = "Walnuts"
                    }
                }
            }
        };

        //store list of products in memory for now
        public static List<NetworkResources.Product> Products = new List<NetworkResources.Product>
        {
            new NetworkResources.Product { Id = 1, Name = "Walnuts", Price = 60.00m },
            new NetworkResources.Product { Id = 2, Name = "Pineapples", Price = 20.00m },
            new NetworkResources.Product { Id = 3, Name = "Coconuts", Price = 30.00m },
            new NetworkResources.Product { Id = 4, Name = "Peanuts", Price = 40.00m },
            new NetworkResources.Product { Id = 5, Name = "Pistachios", Price = 60.00m }
        };
    }
}
