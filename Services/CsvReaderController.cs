using CsvHelper;
using Data.Models;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CsvReaderController
    {
        private readonly CsvReaderService _reader;
        private string directoryPath = "D:\\VTU software engineering\\C#\\CsvFiles\\";
        private string destinantionDir = "D:\\VTU software engineering\\C#\\CsvFiles\\readed\\";

        public CsvReaderController(CsvReaderService reader)
        {
            _reader = reader;
        }
        public List<CsvData> Read()
        {
           return _reader.ReadCsvFilesInDirectory(directoryPath,destinantionDir);
        }
    }
}
