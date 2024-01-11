using ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Implementation
{
    public class DatabaseService:IDatabaseService
    {
        private string connectionString;
        private readonly ITableService tableService;

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ExecuteNonQuery(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        public SQLiteDataReader ExecuteReader(string query)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(query, connection);
            return command.ExecuteReader();
        }

        public T GetEntityById<T>(int entityId, string tableName, string columnName) 
        {
          return tableService.GetEntityById<T>(entityId, tableName,columnName,connectionString);
        }
    }
}
