using CsvHelper;
using Data.Models;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Controllers
{
    public class CsvReaderController
    {
        private readonly ICsvReaderService _reader;
        private string directoryPath = "D:\\VTU software engineering\\C#\\CsvFiles\\";
        private string destinantionDir = "D:\\VTU software engineering\\C#\\CsvFiles\\readed\\";

        public CsvReaderController(ICsvReaderService reader)
        {
            _reader = reader;
        }
        public List<CsvData> Read()
        {
            return _reader.ReadCsvFilesInDirectory(directoryPath, destinantionDir);
        }
    }
}
