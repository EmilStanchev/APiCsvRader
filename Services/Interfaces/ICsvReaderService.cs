using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICsvReaderService
    {
        public List<CsvData> ReadCsvFilesInDirectory(string directoryPath, string destinationDirectory);

    }
}
