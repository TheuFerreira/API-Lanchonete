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

        public SearchFavoritesCase(IProductRepository productRepository, IFileService fileService)
        {
            this.productRepository = productRepository;
            this.fileService = fileService;
        }

        public IEnumerable<SearchFavoritesResponse> Execute(int userId, string search)
        {
            IEnumerable<Product> products = productRepository.GetAllFavorites(userId, search);

            IEnumerable<SearchFavoritesResponse> response = products.Select((x) =>
            {
                float rating = productRepository.GetRatingOfProduct(x.ProductId);

                string photoPath = string.Format("{0}//Photos//Products//Covers//{1}", Directory.GetCurrentDirectory(), x.Photo);
                string photoBase64 = fileService.FileToBase64(photoPath);

                return new SearchFavoritesResponse
                {
                    ProductId = x.ProductId,
                    Title = x.Title,
                    Rating = rating,
                    Price = x.Price,
                    Image = photoBase64,
                    Favorite = true,
                };
            });

            return response;
        }
    }
}
