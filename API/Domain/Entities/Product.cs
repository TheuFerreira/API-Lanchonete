namespace API.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public string PreparationTime { get; set; }
        public bool Disabled { get; set; }

        public Product()
        {
            ProductId = -1;
            Photo = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            Price = 0;
            Calories = 0;
            PreparationTime = string.Empty;
            Disabled = false;
        }
    }
}
