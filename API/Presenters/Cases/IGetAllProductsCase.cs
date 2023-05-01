using API.Presenters.Responses;

namespace API.Presenters.Cases
{
    public interface IGetAllProductsCase
    {
        IEnumerable<GetAllProductsResponse> Execute();
    }
}
