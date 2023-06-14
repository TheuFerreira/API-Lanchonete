using API.Domain.Repositories;
using API.Presenters.Cases;

namespace API.Domain.Cases
{
    public class DeleteProductFromCartCase : IDeleteProductFromCartCase
    {
        private readonly ICartProductRepository cartProductRepository;

        public DeleteProductFromCartCase(ICartProductRepository cartProductRepository)
        {
            this.cartProductRepository = cartProductRepository;
        }

        public void Execute(int userId, int productId)
        {
            cartProductRepository.Delete(userId, productId);
        }
    }
}
