namespace API.Domain.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set;}

        public Coupon()
        {
            CouponId = 0;
            Title = string.Empty;
            Photo = string.Empty;
            DiscountPercentage = 0;
        }
    }
}
