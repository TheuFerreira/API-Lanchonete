using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        float GetRatingOfProduct(int productId);
        int GetTotalRatingsOfProduct(int productId);
        Product? GetById(int productId);
        IEnumerable<string> GetPhotosByProduct(int productId);
    }
}
