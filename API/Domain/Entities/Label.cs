using System.Text.Json.Serialization;

namespace API.Domain.Entities
{
    public class Label
    {
        [JsonPropertyName("label_id")]
        public int LabelId { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public Label()
        {
            LabelId = 0;
            Description = string.Empty;
            Photo = string.Empty;
        }
    }
}
