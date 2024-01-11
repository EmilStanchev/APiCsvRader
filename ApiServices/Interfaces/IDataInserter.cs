using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Interfaces
{
    public interface IDataInserter
    {
        public void InsertData<T>(T data, string connectionString);
        public void UpdateData<T>(T data, string connectionString, string primaryKey);
    }
}
