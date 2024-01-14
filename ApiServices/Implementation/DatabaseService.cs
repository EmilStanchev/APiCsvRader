using ApiDatabaseServices.Interfaces;
using ApiDatabaseServices.ViewModels;
using ApiServices.Interfaces;
using ApiServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Implementation
{
    public class DatabaseService : IDatabaseService
    {
        private string connectionString;
        private readonly IDatabaseConfiguration _config;
        public DatabaseService(string connectionString, IDatabaseConfiguration config)
        {
            this.connectionString = connectionString;
            _config = config;
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
                return _config.TableService.SelectByID<T>(entityId, tableName, columnName, connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public void Start()
        {
            _config.TableCreator.CreateTable<Account>(connectionString);
        }
        public void InsertData<T>(T data)
        {
            _config.DataInserter.InsertData(data, connectionString);
        }
        public void UpdateData<T>(T data, string id)
        {
            _config.DataInserter.UpdateData(data, connectionString, id);
        }
        public List<T> SelectData<T>(string table)
        {
            return _config.TableService.SelectData<T>(table, connectionString);
        }
        public async Task<IEnumerable<Organization>> SearchOrganizationByCountry(string countryId)
        {
            return await _config.StatisticService.SearchOrganizationByCountry(countryId, connectionString);
        }
        public Organization GetOrganizationWithMaxEmployees()
        {
            return _config.StatisticService.GetOrganizationWithMaxEmployees(connectionString);
        }
        public void SoftDeleteCountry(int countryId)
        {
            _config.TableService.SoftDeleteCountry(countryId, connectionString);
        }


    }
}
