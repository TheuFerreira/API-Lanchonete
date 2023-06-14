using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(string search);
        IEnumerable<Product> GetAllByCategories(IEnumerable<int> categories, string search);
        float GetRatingOfProduct(int productId);
        int GetTotalRatingsOfProduct(int productId);
        Product? GetById(int productId);
        IEnumerable<string> GetPhotosByProduct(int productId);
        bool HasFavorite(int productId, int userId);
        bool Favorite(int productId, int userId);
        bool Unfavorite(int productId, int userId);
        IEnumerable<Product> GetAllFavorites(int userId, string search);
    }
}
