using ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Implementation
{
    public class DatabaseService:IDatabaseService
    {
        private string connectionString;
        private readonly ITableService _tableService;

        public DatabaseService(string connectionString,ITableService tableService)
        {
            this.connectionString = connectionString;
            _tableService = tableService;
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

        public T GetEntityById<T>(string entityId, string tableName, string columnName) 
        {
            try
            {
                return _tableService.SelectByID<T>(entityId, tableName, columnName, connectionString);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
    }
}
