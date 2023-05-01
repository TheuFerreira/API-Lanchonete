using API.Presenters.Responses;

namespace API.Presenters.Cases
{
    public interface IGetProductInfoCase
    {
        GetProductInfoResponse Execute(int productId);
    }
}
