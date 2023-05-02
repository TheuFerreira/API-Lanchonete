namespace API.Presenters.Requests
{
    public class GetAllByCategoriesRequest
    {
        public string? Search { get; set; }
        public IEnumerable<int>? Categories { get; set; }
    }
}
