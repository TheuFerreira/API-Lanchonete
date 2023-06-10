using API.Domain.Entities;
using API.Domain.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class SaleProductRepository: ISaleProductRepository
    {
        private readonly IDbConnection connection;

        public SaleProductRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<SaleProduct> BestSellers(int limit)
        {
            string sql = @"
                SELECT id_sale_product AS SaleProductId, id_sale AS SaleId, sp.id_product AS ProductId, sp.price, quantity
                FROM sale_product AS sp
                LEFT JOIN product AS p ON p.id_product = sp.id_product
                WHERE p.disabled = 0
                GROUP BY sp.id_product
                ORDER BY COUNT(sp.id_product) DESC
                LIMIT @limit
            ";

            object data = new
            {
                limit
            };

            IEnumerable<SaleProduct> saleProducts = connection.Query<SaleProduct>(sql, data);
            return saleProducts;
        }

        public IEnumerable<SaleProduct> BestSellersSearch(int limit, string search)
        {
            string sql = $@"
                SELECT id_sale_product AS SaleProductId, id_sale AS SaleId, sp.id_product AS ProductId, sp.price, quantity
                FROM sale_product AS sp
                LEFT JOIN product AS p ON p.id_product = sp.id_product
                WHERE p.disabled = 0 
                    AND p.title LIKE '%{search}%'
                GROUP BY sp.id_product
                ORDER BY COUNT(sp.id_product) DESC
                LIMIT @limit
            ";

            object data = new
            {
                limit
            };

            IEnumerable<SaleProduct> saleProducts = connection.Query<SaleProduct>(sql, data);
            return saleProducts;
        }

        public IEnumerable<SaleProduct> BestSellersSearchAndCategories(int limit, string search, IEnumerable<int> categories)
        {
            string labels = string.Join(",", categories);

            string sql = $@"
                SELECT id_sale_product AS SaleProductId, id_sale AS SaleId, sp.id_product AS ProductId, sp.price, quantity
                FROM sale_product AS sp
                LEFT JOIN product AS p ON p.id_product = sp.id_product
                LEFT JOIN product_label AS pl ON pl.id_product = sp.id_product
                WHERE p.disabled = 0 
                    AND pl.id_label IN ({labels})
                    AND p.title LIKE '%{search}%'
                GROUP BY sp.id_product
                ORDER BY COUNT(sp.id_product) DESC
                LIMIT @limit
            ";

            object data = new
            {
                limit
            };

            IEnumerable<SaleProduct> saleProducts = connection.Query<SaleProduct>(sql, data);
            return saleProducts;
        }
    }
}
