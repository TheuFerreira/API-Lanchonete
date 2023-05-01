using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllByCategories(IEnumerable<int> categories);
        float GetRatingOfProduct(int productId);
        int GetTotalRatingsOfProduct(int productId);
        Product? GetById(int productId);
        IEnumerable<string> GetPhotosByProduct(int productId);
    }
}
