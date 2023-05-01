using API.Domain.Entities;
using System.Text.Json.Serialization;

namespace API.Presenters.Responses
{
    public class GetProductInfoResponse
    {
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }

        [JsonPropertyName("total_ratings")]
        public int TotalRatings { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public float Tax { get; set; }

        [JsonPropertyName("preparation_time")]
        public string PreparationTime { get; set; }
        public IEnumerable<Label> Labels { get; set; }
        public IEnumerable<string> Images { get; set; }

        public GetProductInfoResponse()
        {
            ProductId = -1;
            Title = string.Empty;
            Description = string.Empty;
            Price = 0;
            Rating = 0;
            TotalRatings = 0;
            Calories = 0;
            PreparationTime = string.Empty;
            Labels = new List<Label>();
            Images = new List<string>();
        }
    }
}
