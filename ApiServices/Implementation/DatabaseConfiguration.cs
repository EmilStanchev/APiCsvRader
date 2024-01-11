using ApiDatabaseServices.Interfaces;
using ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Implementation
{
    public class DatabaseConfiguration:IDatabaseConfiguration
    {
        public ITableService TableService { get; set; }
        public ITableCreator TableCreator { get; set; }
        public IDataInserter DataInserter { get; set; }

        public DatabaseConfiguration(ITableCreator creator,ITableService service,IDataInserter dataInserter) 
        {
            TableCreator=creator;
            TableService=service;
            DataInserter = dataInserter;
        }
    }
}
