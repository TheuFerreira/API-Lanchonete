namespace API.Domain.Repositories
{
    public interface IFavoriteRepository
    {
        bool HasFavorite(int productId, int userId);
        bool Favorite(int productId, int userId);
        bool Unfavorite(int productId, int userId);
        int CountFavorites(int userId);
        IEnumerable<int> GetAllFavorites(int userId, string search);
    }
}
