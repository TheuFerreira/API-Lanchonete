using API.Domain.Entities;
using API.Domain.Errors;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Presenters.Cases;
using API.Presenters.Responses;

namespace API.Domain.Cases
{
    public class GetAllProductsBestSellersByCategoriesCase : IGetAllProductsBestSellersByCategoriesCase
    {
        private readonly ISaleProductRepository saleProductRepository;
        private readonly IProductRepository productRepository;
        private readonly IFileService fileService;

        public GetAllProductsBestSellersByCategoriesCase(ISaleProductRepository saleProductRepository, IProductRepository productRepository, IFileService fileService)
        {
            this.saleProductRepository = saleProductRepository;
            this.productRepository = productRepository;
            this.fileService = fileService;
        }

        public IEnumerable<GetAllProductsBestSellersResponse> Execute(IEnumerable<int> categories, string search, int limit)
        {
            IEnumerable<SaleProduct> saleProducts;

            if (categories.Any())
            {
                saleProducts = saleProductRepository.BestSellersSearchAndCategories(limit, search, categories);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                saleProducts = saleProductRepository.BestSellersSearch(limit, search);
            }
            else
            {
                saleProducts = saleProductRepository.BestSellers(limit);
            }

            IEnumerable<GetAllProductsBestSellersResponse> response = saleProducts.Select(x =>
            {
                Product product = productRepository.GetById(x.ProductId) ?? throw new BaseNotFoundException();
                float rating = productRepository.GetRatingOfProduct(x.ProductId);

                string photoPath = string.Format("{0}//Photos//Products//Covers//{1}", Directory.GetCurrentDirectory(), product.Photo);
                string photoBase64 = fileService.FileToBase64(photoPath);

                return new GetAllProductsBestSellersResponse
                {
                    ProductId = x.ProductId,
                    Title = product.Title,
                    Rating = rating,
                    Price = x.Price,
                    Image = photoBase64,
                };
            });

            return response;
        }
    }
}
