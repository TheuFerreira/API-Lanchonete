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

        public IEnumerable<Product> GetAll()
        {
            string sql = @"
                SELECT id_product AS ProductId, photo, title, price
                FROM product
                WHERE disabled = 0;
            ";

            IEnumerable<Product> products = connection.Query<Product>(sql);
            return products;
        }

        public Product? GetById(int productId)
        {
            string sql = @"
                SELECT id_product AS ProductId, title, description, price, calories, preparation_time AS PreparationTime
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
    }
}
