using System.Text.Json.Serialization;

namespace API.Presenters.Responses
{
    public class GetAllLabelsResponse
    {
        [JsonPropertyName("label_id")]
        public int LabelId { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public GetAllLabelsResponse() 
        {
            LabelId = 0;
            Description = string.Empty;
            Photo = string.Empty;
        }
    }
}
