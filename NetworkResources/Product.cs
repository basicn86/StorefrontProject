namespace NetworkResources
{
    public record Product
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string? ProductImageUrl { get; init; }
    }
}
