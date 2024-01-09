using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class DataInserter: IDataInserter
    {
        /*  public void InsertCsvDataListWithoutDuplicates(List<CsvData> csvDataList, SQLiteConnection connection)
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
          }*/

        public void InsertCsvDataListWithoutDuplicates(List<CsvData> csvDataList, SQLiteConnection connection)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    using (var command = new SQLiteCommand(connection))
                    {
                        HashSet<string> existingCountryNames = GetExistingCountryNames(command);

                        HashSet<string> existingOrganizationIds = GetExistingOrganizationIds(command);

                        BatchInsertCountries(command, csvDataList, existingCountryNames);

                        Dictionary<string, int> countryIdMap = GetCountryIds(command);

                        BatchInsertOrganizations(command, csvDataList, countryIdMap, existingOrganizationIds);
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
        private Dictionary<string, int> GetCountryIds(SQLiteCommand command)
        {
            Dictionary<string, int> countryIdMap = new Dictionary<string, int>();
            HashSet<string> uniqueCountryNames = new HashSet<string>();

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

            return countryIdMap;
        }
        private HashSet<string> GetExistingCountryNames(SQLiteCommand command)
        {
            HashSet<string> existingCountryNames = new HashSet<string>();

            command.CommandText = "SELECT CountryName FROM Countries";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string countryName = reader["CountryName"].ToString();
                    existingCountryNames.Add(countryName);
                }
            }

            return existingCountryNames;
        }

        private HashSet<string> GetExistingOrganizationIds(SQLiteCommand command)
        {
            HashSet<string> existingOrganizationIds = new HashSet<string>();

            command.CommandText = "SELECT Organization_Id FROM Organizations";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string organizationId = reader["Organization_Id"].ToString();
                    existingOrganizationIds.Add(organizationId);
                }
            }

            return existingOrganizationIds;
        }

        private void BatchInsertCountries(SQLiteCommand command, List<CsvData> csvDataList, HashSet<string> existingCountryNames)
        {
            command.CommandText = "INSERT OR IGNORE INTO Countries (CountryName) VALUES (@CountryName)";
            command.Parameters.Add("@CountryName", DbType.String);

            foreach (var csvData in csvDataList)
            {
                if (!existingCountryNames.Contains(csvData.Country))
                {
                    command.Parameters["@CountryName"].Value = csvData.Country;
                    command.ExecuteNonQuery();
                    existingCountryNames.Add(csvData.Country);
                }
            }
        }

        private void BatchInsertOrganizations(SQLiteCommand command, List<CsvData> csvDataList, Dictionary<string, int> countryIdMap, HashSet<string> existingOrganizationIds)
        {
            command.CommandText = @"INSERT OR IGNORE INTO Organizations 
                            (""Index"", Organization_Id, Name, Website, Description, Founded, Industry, NumberOfEmployees, CountryId) 
                            VALUES 
                            (@Index, @Organization_Id, @Name, @Website, @Description, @Founded, @Industry, @NumberOfEmployees, @CountryId)";

            command.Parameters.Add("@Index", DbType.Int32);
            command.Parameters.Add("@Organization_Id", DbType.String);
            command.Parameters.Add("@Name", DbType.String);
            command.Parameters.Add("@Website", DbType.String);
            command.Parameters.Add("@Description", DbType.String);
            command.Parameters.Add("@Founded", DbType.Int32);
            command.Parameters.Add("@Industry", DbType.String);
            command.Parameters.Add("@NumberOfEmployees", DbType.Int32);
            command.Parameters.Add("@CountryId", DbType.Int32);

            foreach (var csvData in csvDataList)
            {
                if (!existingOrganizationIds.Contains(csvData.Organization_Id))
                {
                    int countryId = countryIdMap[csvData.Country];

                    command.Parameters["@Index"].Value = csvData.Index;
                    command.Parameters["@Organization_Id"].Value = csvData.Organization_Id;
                    command.Parameters["@Name"].Value = csvData.Name;
                    command.Parameters["@Website"].Value = csvData.Website;
                    command.Parameters["@Description"].Value = csvData.Description;
                    command.Parameters["@Founded"].Value = csvData.Founded;
                    command.Parameters["@Industry"].Value = csvData.Industry;
                    command.Parameters["@NumberOfEmployees"].Value = csvData.NumberOfEmployees;
                    command.Parameters["@CountryId"].Value = countryId;

                    command.ExecuteNonQuery();
                    existingOrganizationIds.Add(csvData.Organization_Id);
                }
            }
        }

    }
}
