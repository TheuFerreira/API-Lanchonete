namespace API.Presenters.Requests
{
    public class GetAllBestSellersByCategoriesRequest
    {
        public string Search { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public int Limit { get; set; }

        public GetAllBestSellersByCategoriesRequest()
        {
            Search = string.Empty;
            Categories = new List<int>();
        }
    }
}
