using API.Domain.Entities;
using API.Domain.Errors;
using API.Domain.Repositories;
using API.Presenters.Cases;
using API.Presenters.Requests;

namespace API.Domain.Cases
{
    public class SaveProductToCartCase : ISaveProductToCartCase
    {
        private readonly IProductRepository productRepository;
        private readonly ICartProductRepository cartProductRepository;

        public SaveProductToCartCase(IProductRepository productRepository, ICartProductRepository cartProductRepository)
        {
            this.productRepository = productRepository;
            this.cartProductRepository = cartProductRepository;
        }

        public void Execute(CartAddRequest request)
        {
            if (request.Quantity <= 0)
                throw new BaseInvalidException();

            _ = productRepository.GetById(request.ProductId) ?? throw new BaseNotFoundException();

            CartProduct? cardProduct = cartProductRepository.Get(request.UserId, request.ProductId);
            if (cardProduct == null)
            {
                cardProduct = new()
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                };
                cartProductRepository.Save(cardProduct);
            }
            else
            {
                cardProduct.UserId = request.UserId;
                cardProduct.ProductId = request.ProductId;
                cardProduct.Quantity += request.Quantity;
                cartProductRepository.Update(cardProduct);
            }
        }
    }
}
