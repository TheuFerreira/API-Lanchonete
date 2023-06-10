using API.Domain.Entities;
using API.Domain.Errors;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Presenters.Cases;
using API.Presenters.Responses;

namespace API.Domain.Cases
{
    public class GetProductInfoCase : IGetProductInfoCase
    {
        private readonly IProductRepository productRepository;
        private readonly IFileService fileService;
        private readonly ILabelRepository labelRepository;
        private readonly ISettingsRepository settingsRepository;

        public GetProductInfoCase(IProductRepository productRepository, IFileService fileService, ILabelRepository labelRepository, ISettingsRepository settingsRepository)
        {
            this.productRepository = productRepository;
            this.fileService = fileService;
            this.labelRepository = labelRepository;
            this.settingsRepository = settingsRepository;
        }

        public GetProductInfoResponse Execute(int productId, int? userId)
        {
            Product? product = productRepository.GetById(productId) ?? throw new BaseNotFoundException();

            float rating = productRepository.GetRatingOfProduct(product.ProductId);
            int totalRating = productRepository.GetTotalRatingsOfProduct(product.ProductId);
            float tax = settingsRepository.GetTax(1);
            IEnumerable<Label> labels = labelRepository.GetAllOfProduct(product.ProductId);
            IEnumerable<string> photos = productRepository.GetPhotosByProduct(product.ProductId);

            IList<string> carousellImages = GetCarousellImages(productId, photos);
            labels = GetLabelsImages(labels);

            bool favorite = false;
            if (userId.HasValue) 
                favorite = productRepository.HasFavorite(productId, userId.Value);

            return new GetProductInfoResponse
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Rating = rating,
                TotalRatings = totalRating,
                Price = product.Price,
                Calories = product.Calories,
                Tax = tax,
                PreparationTime = product.PreparationTime,
                Labels = labels,
                Images = carousellImages,
                Favorite = favorite,
            };
        }

        private IList<string> GetCarousellImages(int productId, IEnumerable<string> photos)
        {
            IList<string> images = new List<string>();
            foreach (string photo in photos)
            {
                string photoPath = string.Format("{0}//Photos//Products//Carousell//{1}//{2}", Directory.GetCurrentDirectory(), productId, photo);
                string photoBase64 = fileService.FileToBase64(photoPath);

                images.Add(photoBase64);
            }

            return images;
        }

        private IList<Label> GetLabelsImages(IEnumerable<Label> labels)
        {
            IList<Label> ls = new List<Label>();
            foreach (Label label in labels)
            {
                string photoPath = string.Format("{0}//Photos//Labels//{1}", Directory.GetCurrentDirectory(), label.Photo);
                label.Photo = fileService.FileToBase64(photoPath);

                ls.Add(label);
            }

            return ls;
        }
    }
}
