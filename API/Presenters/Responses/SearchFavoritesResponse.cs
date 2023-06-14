using System.Text.Json.Serialization;

namespace API.Presenters.Responses
{
    public class SearchFavoritesResponse
    {
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool Favorite { get; set; }

        public SearchFavoritesResponse()
        {
            ProductId = -1;
            Title = string.Empty;
            Price = 0;
            Rating = 0;
            Image = string.Empty;
            Favorite = false;
        }
    }
}
