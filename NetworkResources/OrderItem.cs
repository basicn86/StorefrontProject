namespace NetworkResources
{
    public record OrderItem
    {
        //public ID
        public int Id { get; init; }
        //reference to product ID, can be set by the client
        public int OrderId { get; init; }
        //quantity of product, can be set by the client
        public int Quantity { get; init; }
        //price of product, to be set by the server
        public decimal Price { get; init; }
        //name of product, to be set by the server
        public string Name { get; init; } = string.Empty;
    }
}
