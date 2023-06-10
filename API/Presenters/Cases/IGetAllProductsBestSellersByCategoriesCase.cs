using API.Presenters.Responses;

namespace API.Presenters.Cases
{
    public interface IGetAllProductsBestSellersByCategoriesCase
    {
        IEnumerable<GetAllProductsBestSellersResponse> Execute(IEnumerable<int> categories, string search, int limit);
    }
}
