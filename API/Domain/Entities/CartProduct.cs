namespace API.Domain.Entities
{
    public class CartProduct
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
