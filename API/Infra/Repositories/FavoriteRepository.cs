using API.Domain.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly IDbConnection connection;

        public FavoriteRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public int CountFavorites(int userId)
        {
            string sql = "SELECT COUNT(id_product) FROM product_favorite WHERE id_user = @userId;";
            object data = new { userId };

            int count = connection.ExecuteScalar<int>(sql, data);
            return count;
        }

        public bool Favorite(int productId, int userId)
        {
            string sql = "INSERT INTO product_favorite (id_product, id_user) VALUES (@productId, @userId);";
            object data = new
            {
                productId,
                userId,
            };

            int count = connection.Execute(sql, data);
            return count == 1;
        }

        public bool HasFavorite(int productId, int userId)
        {
            string sql = @"
                SELECT COUNT(id_product)
                FROM product_favorite
                WHERE id_product = @productId 
                    AND id_user = @userId
            ";

            object data = new
            {
                productId,
                userId
            };

            int result = connection.ExecuteScalar<int>(sql, data);
            return result == 1;
        }

        public bool Unfavorite(int productId, int userId)
        {
            string sql = "DELETE FROM product_favorite WHERE id_product = @productId AND id_user = @userId;";
            object data = new
            {
                productId,
                userId,
            };

            int count = connection.Execute(sql, data);
            return count == 1;
        }

        public IEnumerable<int> GetAllFavorites(int userId, string search)
        {
            string sql = $@"
                SELECT p.id_product 
                FROM product AS p
                INNER JOIN product_favorite AS pf ON pf.id_product = p.id_product
                WHERE p.disabled = 0
                    AND p.title LIKE '%{search}%'
                    AND pf.id_user = @userId;
            ";

            object data = new
            {
                userId
            };

            IEnumerable<int> products = connection.Query<int>(sql, data);
            return products;
        }
    }
}
