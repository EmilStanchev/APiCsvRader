using Data.Implementation;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Implementations;

namespace Services.Controllers
{
    public class ReaderController
    {
        private readonly ReaderService _readerService;
        private string dbPath = "D:\\VTU software engineering\\C#\\API\\CsvUniProject\\APiCsvRader\\CsvReader\\database.db";
        public ReaderController(ReaderService readerService)
        {
            _readerService = readerService;
        }
        public void Start ()
        {
            _readerService.Start();
        }
    }
}
