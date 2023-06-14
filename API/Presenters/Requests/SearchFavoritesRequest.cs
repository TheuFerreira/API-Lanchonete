namespace API.Presenters.Requests
{
    public class SearchFavoritesRequest
    {
        public string Search { get; set; }

        public SearchFavoritesRequest()
        {
            Search = string.Empty;
        }
    }
}
