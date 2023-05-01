using API.Presenters.Responses;

namespace API.Presenters.Cases
{
    public interface IGetAllLabelsCase
    {
        IEnumerable<GetAllLabelsResponse> Execute();
    }
}
