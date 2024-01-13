using ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Interfaces
{
    public interface IDatabaseConfiguration
    {

        public ITableService TableService { get; set; }
        public ITableCreator TableCreator { get; set; }
        public IDataInserter DataInserter { get; set; }
        public IStatisticService StatisticService { get; set; }

    }
}
