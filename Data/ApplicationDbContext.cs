using Data.Implementation;
using Data.Interfaces;
using Data.Models;
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
        private readonly IDatabaseService databaseService;
        private readonly IDataInserter _dataInserter;
        public ApplicationDbContext(string databasePath,IDatabaseService service, IDataInserter dataInserter)
        {
            connectionString = $"Data Source={databasePath};Version=3;";
            databaseService = service;
            connection = new SQLiteConnection(connectionString);
            _dataInserter = dataInserter;
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
        public void InsertCsvDataListWithoutDuplicates(List<CsvData> csvDataList)
        {
            _dataInserter.InsertCsvDataListWithoutDuplicates(csvDataList,connection);
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
