using API.Presenters.Responses;

namespace API.Presenters.Cases
{
    public interface ISearchFavoritesCase
    {
        IEnumerable<SearchFavoritesResponse> Execute(int userId, string search);
    }
}
