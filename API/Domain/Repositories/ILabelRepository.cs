using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface ILabelRepository
    {
        IEnumerable<Label> GetAllOfProduct(int productId);
        IEnumerable<Label> GetAll();
    }
}
