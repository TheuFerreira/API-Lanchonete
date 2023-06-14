using API.Domain.Repositories;
using API.Presenters.Cases;

namespace API.Domain.Cases
{
    public class CountFavoritesCase : ICountFavoritesCase
    {
        private readonly IFavoriteRepository favoriteRepository;

        public CountFavoritesCase(IFavoriteRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }

        public int Execute(int userId)
        {
            int count = favoriteRepository.CountFavorites(userId);
            return count;
        }
    }
}
