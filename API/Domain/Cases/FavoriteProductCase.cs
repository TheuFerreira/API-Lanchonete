using API.Domain.Repositories;
using API.Presenters.Cases;

namespace API.Domain.Cases
{
    public class FavoriteProductCase : IFavoriteProductCase
    {
        private readonly IFavoriteRepository favoriteRepository;

        public FavoriteProductCase(IFavoriteRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }

        public bool Execute(int userId, int productId)
        {
            bool hasFavorite = favoriteRepository.HasFavorite(productId, userId);
            if (hasFavorite)
            {
                favoriteRepository.Unfavorite(productId, userId);
            }
            else
            {
                favoriteRepository.Favorite(productId, userId);
            }

            bool newValue = !hasFavorite;
            return newValue;
        }
    }
}
