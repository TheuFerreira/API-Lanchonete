using API.Domain.Repositories;
using API.Presenters.Cases;

namespace API.Domain.Cases
{
    public class CountFavoritesCase : ICountFavoritesCase
    {
        private readonly IProductRepository productRepository;

        public CountFavoritesCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public int Execute(int userId)
        {
            int count = productRepository.CountFavorites(userId);
            return count;
        }
    }
}
