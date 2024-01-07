using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class DataInserter: IDataInserter
    {
        public void InsertCsvDataListWithoutDuplicates(List<CsvData> csvDataList, SQLiteConnection connection)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    using (var command = new SQLiteCommand(connection))
                    {

                        command.CommandText = "INSERT OR IGNORE INTO Countries (CountryName) VALUES (@CountryName)";
                        HashSet<string> uniqueCountries = new HashSet<string>();

                        foreach (var csvData in csvDataList)
                        {
                            if (!uniqueCountries.Contains(csvData.Country))
                            {
                                uniqueCountries.Add(csvData.Country);
                                command.Parameters.AddWithValue("@CountryName", csvData.Country);
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                            }
                        }

                        Dictionary<string, int> countryIdMap = GetCountryIds(connection);
                        command.CommandText = @"INSERT OR IGNORE INTO Organizations 
                                        (""Index"", Organization_Id, Name, Website, Description, Founded, Industry, NumberOfEmployees, CountryId) 
                                        VALUES 
                                        (@Index, @Organization_Id, @Name, @Website, @Description, @Founded, @Industry, @NumberOfEmployees, @CountryId)";

                        foreach (var csvData in csvDataList)
                        {
                            int countryId = countryIdMap[csvData.Country];

                            command.Parameters.AddWithValue("@Index", csvData.Index);
                            command.Parameters.AddWithValue("@Organization_Id", csvData.Organization_Id);
                            command.Parameters.AddWithValue("@Name", csvData.Name);
                            command.Parameters.AddWithValue("@Website", csvData.Website);
                            command.Parameters.AddWithValue("@Description", csvData.Description);
                            command.Parameters.AddWithValue("@Founded", csvData.Founded);
                            command.Parameters.AddWithValue("@Industry", csvData.Industry);
                            command.Parameters.AddWithValue("@NumberOfEmployees", csvData.NumberOfEmployees);
                            command.Parameters.AddWithValue("@CountryId", countryId);

                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    stopwatch.Stop();
                    Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} milliseconds for insert in db");
                }
            }
        }

        private Dictionary<string, int> GetCountryIds(SQLiteConnection connection)
        {
            Dictionary<string, int> countryIdMap = new Dictionary<string, int>();
            HashSet<string> uniqueCountryNames = new HashSet<string>();

            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT CountryName, Id FROM Countries";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string countryName = reader["CountryName"].ToString();
                        int countryId = Convert.ToInt32(reader["Id"]);

                        if (!uniqueCountryNames.Contains(countryName))
                        {
                            countryIdMap.Add(countryName, countryId);
                            uniqueCountryNames.Add(countryName);
                        }
                    }
                }
            }

            return countryIdMap;
        }
    }
}
