using Data.Interfaces;
using Services.Controllers;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IReaderConfiguration
    {
        public IDatabaseService DatabaseService { get; set; }
        public IDataInserter Inserter { get; set; }
        public CsvReaderService CsvReaderService { get; set; }
        public CsvReaderController Controller { get; set; }
        string databasePath { get; }
    }
}
