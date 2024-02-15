using System.Text.Json.Serialization;

namespace StorefrontAPI.Models
{
    public class Product
    {
        [JsonInclude]
        public int Id { get; set; }
        [JsonInclude]
        public string Name { get; set; }
        [JsonInclude]
        public decimal Price { get; set; }
    }
}
