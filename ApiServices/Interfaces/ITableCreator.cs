using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Interfaces
{
    public interface ITableCreator
    {
        public void CreateTable<T>(string connectionString);
        public void DeleteTable<T>(string connectionString);
    }
}
