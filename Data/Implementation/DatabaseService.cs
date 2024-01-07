using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Data.Interfaces;

namespace Data.Implementation
{
    public class DatabaseService:IDatabaseService
    {
        public DataTable ExecuteQuery(string query,SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var adapter = new SQLiteDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        public void ExecuteNonQuery(string query, SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(query,connection))
            {
                command.ExecuteNonQuery();
            }
        }
        public void CreateDb(SQLiteConnection connection, string connectionString)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            connection = new SQLiteConnection(connectionString);
            connection.Open();

            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Countries (
                                 Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                 CountryName TEXT)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE IF NOT EXISTS Organizations (
                                 ""Index"" INTEGER,
                                 Organization_Id TEXT,
                                 Name TEXT,
                                 Website TEXT,
                                 Description TEXT,
                                 Founded INTEGER,
                                 Industry TEXT,
                                 NumberOfEmployees INTEGER,
                                 CountryId INTEGER,
                                 FOREIGN KEY (CountryId) REFERENCES Countries(Id))";
                command.ExecuteNonQuery();
            }

            stopwatch.Stop();
            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} milliseconds for creating db");
        }
    }
}
