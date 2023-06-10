namespace API.Domain.Entities
{
    public class SaleProduct
    {
        public int SaleProductId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
