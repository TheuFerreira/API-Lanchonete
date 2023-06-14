using System.Text.Json.Serialization;

namespace API.Presenters.Requests
{
    public class CartAddRequest
    {
        [JsonIgnore]
        public int UserId { get; set; }

        [JsonPropertyName("id_product")]
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
