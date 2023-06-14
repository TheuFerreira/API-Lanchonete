using API.Domain.Entities;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Presenters.Cases;
using API.Presenters.Responses;

namespace API.Domain.Cases
{
    public class SearchFavoritesCase : ISearchFavoritesCase
    {
        private readonly IProductRepository productRepository;
        private readonly IFileService fileService;
        private readonly IFavoriteRepository favoriteRepository;

        public SearchFavoritesCase(IProductRepository productRepository, IFileService fileService, IFavoriteRepository favoriteRepository)
        {
            this.productRepository = productRepository;
            this.fileService = fileService;
            this.favoriteRepository = favoriteRepository;
        }

        public IEnumerable<SearchFavoritesResponse> Execute(int userId, string search)
        {
            IEnumerable<int> favorites = favoriteRepository.GetAllFavorites(userId, search);
            IEnumerable<Product?> products = favorites.Select(productRepository.GetById);

            IList<SearchFavoritesResponse> responses = new List<SearchFavoritesResponse>();
            foreach (Product? product in products)
            {
                if (product == null) continue;

                float rating = productRepository.GetRatingOfProduct(product.ProductId);

                string photoPath = string.Format("{0}//Photos//Products//Covers//{1}", Directory.GetCurrentDirectory(), product.Photo);
                string photoBase64 = fileService.FileToBase64(photoPath);

                SearchFavoritesResponse response = new()
                {
                    ProductId = product.ProductId,
                    Title = product.Title,
                    Rating = rating,
                    Price = product.Price,
                    Image = photoBase64,
                    Favorite = true,
                };

                responses.Add(response);
            }

            return responses;
        }
    }
}
