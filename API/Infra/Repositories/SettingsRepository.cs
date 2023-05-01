using API.Domain.Repositories;
using Dapper;
using MySqlConnector;
using System.Data;

namespace API.Infra.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IDbConnection connection;

        public SettingsRepository()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=lanchonete;UID=root;PWD=root;";
            connection = new MySqlConnection(connectionString);
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
