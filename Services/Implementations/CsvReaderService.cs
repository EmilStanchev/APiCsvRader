using CsvHelper;
using Data.Interfaces;
using Data.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CsvReaderService: ICsvReaderService
    {
        public List<CsvData> ReadCsvFilesInDirectory(string directoryPath, string destinationDirectory)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<CsvData> allCsvData = new List<CsvData>();

            try
            {
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");

                foreach (var filePath in csvFiles)
                {
                    try
                    {
                        using (var reader = new StreamReader(filePath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            try
                            {
                                var records = csv.GetRecords<CsvData>().ToList();
                                allCsvData.AddRange(records);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error reading CSV file '{filePath}': {ex.Message}");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing CSV file '{filePath}': {ex.Message}");
                    }
                    try
                    {
                        MoveFile(filePath, destinationDirectory);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                stopwatch.Stop();
                Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} milliseconds for reading");

                return allCsvData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV files in directory: {ex.Message}");
            }
            return allCsvData;
        }
        private void MoveFile(string sourceFilePath, string destinationDirectory)
        {
            try
            {
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
                string destinationPath = Path.Combine(destinationDirectory, Path.GetFileName(sourceFilePath));
                File.Move(sourceFilePath, destinationPath);

                Console.WriteLine($"File '{Path.GetFileName(sourceFilePath)}' moved to '{destinationDirectory}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file '{sourceFilePath}': {ex.Message}");
            }
        }
    }
}
