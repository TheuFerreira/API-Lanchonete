using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface ICartProductRepository
    {
        CartProduct? Get(int userId, int productId);
        int Save(CartProduct product);
        int Update(CartProduct product);
        int Count(int userId);
    }
}
