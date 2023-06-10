using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface ISaleProductRepository
    {
        IEnumerable<SaleProduct> BestSellers(int limit);
        IEnumerable<SaleProduct> BestSellersSearch(int limit, string search);
        IEnumerable<SaleProduct> BestSellersSearchAndCategories(int limit, string search, IEnumerable<int> categories);
    }
}
