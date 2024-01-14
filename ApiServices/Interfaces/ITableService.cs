using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Interfaces
{
    public interface ITableService
    {
        public T SelectByID<T>(string table, string id, string columnName, string connectionString);
        public List<T> SelectData<T>(string table, string connectionString);
        public void SoftDeleteCountry(int countryId, string connectionString);
    }
}
