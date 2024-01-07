using Data.Implementation;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Services.Interfaces;
using Services.Controllers;

namespace Services.Implementations
{
    public class ReaderService
    {
        private readonly IReaderConfiguration _config;
        public ReaderService(IReaderConfiguration config)
        {
            _config = config;
        }

        public void Start()
        {
            var csvData = _config.Controller.Read();
            using (var dbContext = new ApplicationDbContext(_config.databasePath, _config.DatabaseService, _config.Inserter))
            {
                dbContext.Start();
                dbContext.InsertCsvDataListWithoutDuplicates(csvData);
            }
        }
    }
}
