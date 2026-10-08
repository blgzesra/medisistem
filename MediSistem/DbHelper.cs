using System.Configuration;
using MySql.Data.MySqlClient;

namespace MediSistem
{
    public static class DbHelper
    {
        // Bağlantı dizesi App.config'deki "MediSistemDb" ayarından okunur.
        // Yerel/gerçek şifre App.local.config dosyasında tutulur (git'e girmez).
        private const string DefaultConnectionString =
            "Server=localhost;Database=medisistem_db;Uid=root;Pwd=;Charset=utf8;";

        private static readonly string connectionString =
            ConfigurationManager.AppSettings["MediSistemDb"] ?? DefaultConnectionString;

        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
