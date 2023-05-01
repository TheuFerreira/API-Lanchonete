using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface ICouponRepository
    {
        IEnumerable<Coupon> GetAllNotExpired();
    }
}
