namespace API.Presenters.Cases
{
    public interface IDeleteProductFromCartCase
    {
        public void Execute(int userId, int productId);
    }
}
