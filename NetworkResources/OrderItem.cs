namespace NetworkResources
{
    public record OrderItem
    {
        //reference to product ID
        public int ProductId { get; init; }
        //quantity of product
        public int Quantity { get; init; }
        //price of product
        public decimal Price { get; init; }
        //name of product
        public string Name { get; init; } = string.Empty;
    }
}
