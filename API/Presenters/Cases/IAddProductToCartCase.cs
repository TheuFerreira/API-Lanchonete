using API.Presenters.Requests;

namespace API.Presenters.Cases
{
    public interface IAddProductToCartCase
    {
        public void Execute(CartAddRequest request);
    }
}
