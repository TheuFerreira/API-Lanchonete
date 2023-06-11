using API.Domain.Repositories;
using API.Presenters.Cases;

namespace API.Domain.Cases
{
    public class FavoriteProductCase : IFavoriteProductCase
    {
        private readonly IProductRepository productRepository;

        public FavoriteProductCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public bool Execute(int userId, int productId)
        {
            bool hasFavorite = productRepository.HasFavorite(productId, userId);
            if (hasFavorite)
            {
                productRepository.Unfavorite(productId, userId);
            }
            else
            {
                productRepository.Favorite(productId, userId);
            }

            bool newValue = !hasFavorite;
            return newValue;
        }
    }
}
