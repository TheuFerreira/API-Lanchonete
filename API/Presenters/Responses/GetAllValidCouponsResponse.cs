using System.Text.Json.Serialization;

namespace API.Presenters.Responses
{
    public class GetAllValidCouponsResponse
    {
        [JsonPropertyName("coupon_id")]
        public int CouponId { get; set; }
        public string Image { get; set; }

        public GetAllValidCouponsResponse()
        {
            CouponId = 0;
            Image = string.Empty;
        }
    }
}
