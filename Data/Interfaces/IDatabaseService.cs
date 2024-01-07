using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDatabaseService
    {

        public void CreateDb(SQLiteConnection connection, string connectionString);
        public DataTable ExecuteQuery(string query, SQLiteConnection connection);
        public void ExecuteNonQuery(string query, SQLiteConnection connection);

    }
}
