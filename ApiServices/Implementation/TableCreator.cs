using ApiDatabaseServices.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiServices.Implementation
{
    public class TableCreator:ITableCreator
    {
        public void CreateTable<T>(string connectionString)
        {
            try
            {
                string tableName = typeof(T).Name;
                var columns = GetColumnsFromType<T>();
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = $"CREATE TABLE IF NOT EXISTS {tableName}s (Id TEXT PRIMARY KEY, {string.Join(", ", columns)})";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteTable<T>(string connectionString)
        {
            string tableName = typeof(T).Name;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = $"DROP TABLE IF EXISTS {tableName}";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private IEnumerable<string> GetColumnsFromType<T>()
        {
            return typeof(T)
                .GetProperties()
                .Select(property =>
                {
                    string columnName = property.Name;
                    string columnType = GetSqliteType(property.PropertyType);
                    return $"{columnName} {columnType}";
                });
        }

        private string GetSqliteType(Type propertyType)
        {
            // Add more type mappings as needed
            if (propertyType == typeof(int) || propertyType == typeof(long))
                return "INTEGER";
            else if (propertyType == typeof(string))
                return "TEXT";
            else if (propertyType == typeof(DateTime))
                return "DATETIME";
            else
                return "TEXT"; 
        }
    }
}
