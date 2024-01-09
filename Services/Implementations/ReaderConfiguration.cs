using Data.Interfaces;
using Services.Controllers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ReaderConfiguration: IReaderConfiguration
    {
        public IDatabaseService DatabaseService { get; set; }
        public IDataInserter Inserter { get; set; }
        public CsvReaderService CsvReaderService { get; set; }
        public CsvReaderController Controller { get; set; }
        public string databasePath => "D:\\VTU software engineering\\C#\\API\\CsvUniProject\\APiCsvRader\\CsvReader\\database.db";
        public ReaderConfiguration(IDatabaseService databaseService, IDataInserter inserter, CsvReaderService csvReaderService, CsvReaderController controller)
        {
            DatabaseService = databaseService;
            Inserter = inserter;
            CsvReaderService = csvReaderService;
            Controller = controller;
        }

    }
}
