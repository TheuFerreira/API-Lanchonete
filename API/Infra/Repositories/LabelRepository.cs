using API.Domain.Entities;
using API.Domain.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly IDbConnection connection;

        public LabelRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<Label> GetAll()
        {
            string sql = @"
                SELECT id_label AS LabelId, description, photo
                FROM label AS pl;
            ";

            IEnumerable<Label> labels = connection.Query<Label>(sql);
            return labels;
        }

        public IEnumerable<Label> GetAllOfProduct(int productId)
        {
            string sql = @"
                SELECT pl.id_label AS LabelId, l.description, l.photo
                FROM product_label AS pl
                LEFT JOIN label AS l ON pl.id_label = l.id_label
                WHERE pl.id_product = @productId;
            ";

            object data = new
            {
                productId
            };

            IEnumerable<Label> labels = connection.Query<Label>(sql, data);
            return labels;
        }
    }
}
