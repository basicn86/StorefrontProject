namespace StorefrontAPI
{
    public static class MemoryDatabase
    {
        //store orders in memory for now
        public static List<NetworkResources.Order> Orders = new List<NetworkResources.Order>();

        //store list of products in memory for now
        public static List<NetworkResources.Product> Products = new List<NetworkResources.Product>
        {
            new NetworkResources.Product { Id = 1, Name = "Walnuts", Price = 50.00m },
            new NetworkResources.Product { Id = 2, Name = "Pineapples", Price = 20.00m },
            new NetworkResources.Product { Id = 3, Name = "Coconuts", Price = 30.00m }
        };
    }
}
