using API.Domain.Entities;
using API.Domain.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IDbConnection connection;

        public CouponRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<Coupon> GetAllNotExpired()
        {
            string sql = @"
                SELECT id_coupon AS CouponId, photo
                FROM coupon
                WHERE expires_at > UTC_TIMESTAMP();
            ";

            IEnumerable<Coupon> coupons = connection.Query<Coupon>(sql);
            return coupons;
        }
    }
}
