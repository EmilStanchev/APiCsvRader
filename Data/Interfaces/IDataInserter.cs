using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDataInserter
    {

        public void InsertCsvDataListWithoutDuplicates(List<CsvData> csvDataList, SQLiteConnection connection);
    }
}
