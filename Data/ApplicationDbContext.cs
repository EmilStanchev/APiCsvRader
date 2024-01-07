using Data.Implementation;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext:IDisposable
    {

        private readonly string connectionString;
        private SQLiteConnection connection;
        private readonly DatabaseService databaseService;
        public ApplicationDbContext(string databasePath,DatabaseService service)
        {
            connectionString = $"Data Source={databasePath};Version=3;";
            databaseService = service;
        }
        public void Start()
        {
            databaseService.CreateDb(connection,connectionString);
        }
        public DataTable ExecuteQuery(string query)
        {
           return databaseService.ExecuteQuery(query,connection);
        }
        public void ExecuteNonQuery(string query)
        {
            databaseService.ExecuteNonQuery(query,connection);
        }
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
