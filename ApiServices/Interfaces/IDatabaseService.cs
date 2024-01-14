using ApiServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Interfaces
{
    public interface IDatabaseService
    {
        public void ExecuteNonQuery(string query);
        public SQLiteDataReader ExecuteReader(string query);

        public T GetEntityById<T>(string entityId, string tableName, string columnName);

        public void InsertData<T>(T data);
        public void UpdateData<T>(T data, string id);
        public List<T> SelectData<T>(string table);
        public Task<IEnumerable<Organization>> SearchOrganizationByCountry(string countryId);
        public Organization GetOrganizationWithMaxEmployees();
        public void SoftDeleteCountry(int countryId);

    }
}
