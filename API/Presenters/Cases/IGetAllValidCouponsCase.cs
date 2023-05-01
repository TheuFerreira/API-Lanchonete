using API.Presenters.Responses;

namespace API.Presenters.Cases
{
    public interface IGetAllValidCouponsCase
    {
        IEnumerable<GetAllValidCouponsResponse> Execute();
    }
}
