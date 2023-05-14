using API.Presenters.Responses;

namespace API.Presenters.Cases
{
    public interface IGetAllProductsByCategoriesCase
    {
        IEnumerable<GetAllProductsResponse> Execute(IEnumerable<int> categories, string search);
    }
}
