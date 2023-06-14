using API.Domain.Entities;
using API.Domain.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class CartProductRepository : ICartProductRepository
    {
        private readonly IDbConnection connection;

        public CartProductRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public int Count(int userId)
        {
            string sql = "SELECT COUNT(id_product) FROM cart_product WHERE id_user = @userId;";
            object data = new { userId };

            int count = connection.ExecuteScalar<int>(sql, data);
            return count;
        }

        public int Delete(int userId, int productId)
        {
            string sql = "DELETE FROM cart_product WHERE id_user = @userId AND id_product = @productId;";
            object data = new { userId, productId};

            int result = connection.Execute(sql, data);
            return result;
        }

        public CartProduct? Get(int userId, int productId)
        {
            string sql = "SELECT quantity FROM cart_product WHERE id_user = @userId AND id_product = @productId";
            object data = new { userId, productId };

            CartProduct? cartProduct = connection.Query<CartProduct>(sql, data).FirstOrDefault();
            return cartProduct;
        }

        public int Save(CartProduct product)
        {
            string sql = "INSERT INTO cart_product (id_user, id_product, quantity) VALUES (@userId, @productId, @quantity);";
            object data = new
            {
                product.UserId,
                product.ProductId,
                product.Quantity,
            };

            int result = connection.Execute(sql, data);
            return result;
        }

        public int Update(CartProduct product)
        {
            string sql = "UPDATE cart_product SET quantity = @quantity WHERE id_user = @userId AND id_product = @productId;";
            object data = new
            {
                product.UserId,
                product.ProductId,
                product.Quantity,
            };

            int result = connection.Execute(sql, data);
            return result;
        }
    }
}
