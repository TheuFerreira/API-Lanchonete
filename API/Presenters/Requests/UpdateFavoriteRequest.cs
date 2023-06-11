using System.Text.Json.Serialization;

namespace API.Presenters.Requests
{
    public class UpdateFavoriteRequest
    {
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }
    }
}
