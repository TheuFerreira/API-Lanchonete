namespace API.Presenters.Cases
{
    public interface IFavoriteProductCase
    {
        public bool Execute(int userId, int productId);
    }
}
