using API.Domain.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IDbConnection connection;

        public SettingsRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public float GetTax(int settingsId)
        {
            string sql = @"SELECT tax FROM settings WHERE id_settings = @settingsId;";

            object data = new
            {
                settingsId
            };

            float tax = connection.QueryFirst<float>(sql, data);
            return tax;
        }
    }
}
