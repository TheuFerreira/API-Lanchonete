using API.Domain.Entities;
using API.Domain.Errors;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Presenters.Cases;
using API.Presenters.Responses;

namespace API.Domain.Cases
{
    public class GetAllProductsByCategoriesCase : IGetAllProductsByCategoriesCase
    {
        private readonly IProductRepository productRepository;
        private readonly IFileService fileService;

        public GetAllProductsByCategoriesCase(IProductRepository productRepository, IFileService fileService)
        {
            this.productRepository = productRepository;
            this.fileService = fileService;
        }

        public IEnumerable<GetAllProductsResponse> Execute(IEnumerable<int> categories, string search)
        {
            IEnumerable<Product> products;
            if (categories.Any())
                products = productRepository.GetAllByCategories(categories, search);
            else
                products = productRepository.GetAll(search);

            if (!products.Any())
                throw new BaseEmptyException();

            IEnumerable<GetAllProductsResponse> response = products.Select(x =>
            {
                float rating = productRepository.GetRatingOfProduct(x.ProductId);

                string photoPath = string.Format("{0}//Photos//Products//Covers//{1}", Directory.GetCurrentDirectory(), x.Photo);
                string photoBase64 = fileService.FileToBase64(photoPath);

                return new GetAllProductsResponse
                {
                    ProductId = x.ProductId,
                    Title = x.Title,
                    Rating = rating,
                    Price = x.Price,
                    Image = photoBase64,
                };
            });

            return response;
        }
    }
}
