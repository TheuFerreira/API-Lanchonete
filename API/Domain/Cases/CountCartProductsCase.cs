using API.Domain.Repositories;
using API.Presenters.Cases;

namespace API.Domain.Cases
{
    public class CountCartProductsCase : ICountCartProductsCase
    {
        private readonly ICartProductRepository cartProductRepository;

        public CountCartProductsCase(ICartProductRepository cartProductRepository)
        {
            this.cartProductRepository = cartProductRepository;
        }

        public int Execute(int userId) 
        { 
            return cartProductRepository.Count(userId);
        }
    }
}
