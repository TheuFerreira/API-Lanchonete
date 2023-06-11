using API.Domain.Entities;
using API.Domain.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection connection;

        public ProductRepository(IDbConnection connection)
        {
            this.connection = connection;
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

        public IEnumerable<Product> GetAll(string search)
        {
            string sql = @$"
                SELECT id_product AS ProductId, photo, title, price
                FROM product
                WHERE disabled = 0
                    AND title LIKE '%{search}%';
            ";

            IEnumerable<Product> products = connection.Query<Product>(sql);
            return products;
        }

        public IEnumerable<Product> GetAllByCategories(IEnumerable<int> categories, string search)
        {
            string labels = string.Join(",", categories);
            string sql = @$"
                SELECT p.id_product AS ProductId, p.photo, p.title, p.price
                FROM product AS p
                INNER JOIN product_label AS pl ON pl.id_product = p.id_product
                WHERE disabled = 0
	                AND pl.id_label IN ({labels})
                    AND p.title LIKE '%{search}%';
            ";

            IEnumerable<Product> products = connection.Query<Product>(sql);
            return products;
        }

        public Product? GetById(int productId)
        {
            string sql = @"
                SELECT id_product AS ProductId, photo, title, description, price, calories, preparation_time AS PreparationTime
                FROM product
                WHERE disabled = 0 
                    AND id_product = @productId;
            ";

            object data = new
            {
                productId
            };

            Product? product = connection.Query<Product>(sql, data).FirstOrDefault();
            return product;
        }

        public IEnumerable<string> GetPhotosByProduct(int productId)
        {
            string sql = @"
                SELECT photo
                FROM product_photo
                WHERE id_product = @productId;
            ";

            object data = new
            {
                productId
            };

            IEnumerable<string> photos = connection.Query<string>(sql, data);
            return photos;
        }

        public float GetRatingOfProduct(int productId)
        {
            string sql = @"
                SELECT IF(AVG(rating) IS NULL, 0, AVG(rating))
                FROM product_rating
                WHERE id_product = @productId;
            ";

            object data = new
            {
                productId
            };

            float rating = connection.QueryFirst<float>(sql, data);
            return rating;
        }

        public int GetTotalRatingsOfProduct(int productId)
        {
            string sql = @"
                SELECT COUNT(rating)
                FROM product_rating
                WHERE id_product = @productId;
            ";

            object data = new
            {
                productId
            };

            int totalRating = connection.QueryFirst<int>(sql, data);
            return totalRating;
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
    }
}
