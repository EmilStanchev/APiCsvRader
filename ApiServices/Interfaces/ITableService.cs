using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Interfaces
{
    public interface ITableService
    {
        public T GetEntityById<T>(int entityId, string tableName, string columnName, string connectionString);
    }
}
