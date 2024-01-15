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
        public IStatisticService StatisticService { get; set; }
        public IDeleteService DeleteService { get; set; }


        public DatabaseConfiguration(ITableCreator creator,ITableService service,IDataInserter dataInserter,IStatisticService statisticService,IDeleteService deleteService) 
        {
            TableCreator=creator;
            TableService=service;
            DataInserter = dataInserter;
            StatisticService=statisticService;
            DeleteService=deleteService;
        }
    }
}
