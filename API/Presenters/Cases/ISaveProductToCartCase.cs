using API.Presenters.Requests;

namespace API.Presenters.Cases
{
    public interface ISaveProductToCartCase
    {
        public void Execute(CartAddRequest request);
    }
}
