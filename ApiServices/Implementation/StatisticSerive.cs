using ApiDatabaseServices.Interfaces;
using ApiServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Diagnostics;

namespace ApiDatabaseServices.Implementation
{
    public class StatisticSerive:IStatisticService
    {
        /*  public List<Organization> SearchOrganizationByCountry(string countryId, string connectionString)
          {
              List<Organization> results = new List<Organization>();
              string query = "SELECT * FROM Organizations INDEXED BY idx_country_id WHERE CountryId = @CountryId";

              using (SQLiteConnection connection = new SQLiteConnection(connectionString))
              {
                  connection.Open();
                  using (SQLiteCommand command = new SQLiteCommand(query, connection))
                  {
                      command.Parameters.AddWithValue("@CountryId", countryId);

                      using (SQLiteDataReader reader = command.ExecuteReader())
                      {
                          while (reader.Read())
                          {
                              Organization organization = new Organization
                              {
                                  Name = reader.GetString(reader.GetOrdinal("Name")),
                                  Website = reader.GetString(reader.GetOrdinal("Website")),
                                  Organization_Id = reader.GetString(reader.GetOrdinal("Organization_Id")),
                                  Description = reader.GetString(reader.GetOrdinal("Description")),
                                  Industry = reader.GetString(reader.GetOrdinal("Industry")),
                                  Founded = reader.GetInt32(reader.GetOrdinal("Founded")),
                                  CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                                  NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees")),
                              };

                              results.Add(organization);
                          }
                      }
                  }
              }
              return results;
          }*/

        public async Task<IEnumerable<Organization>> SearchOrganizationByCountry(string countryId, string connectionString)
        {
            string query = "SELECT * FROM Organizations WHERE CountryId = @CountryId AND IsDeleted = 0";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var res = connection.QueryAsync<Organization>(query, new { CountryId = countryId });
                return res.Result;
            }
        }
        public Organization GetOrganizationWithMaxEmployees(string connectionString)
        {
            Organization organization = null;

            string query = @"
            SELECT * FROM Organizations
            ORDER BY NumberOfEmployees DESC
            LIMIT 1;
        ";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            organization = new Organization
                            {
                                Index = reader.GetInt32(reader.GetOrdinal("Index")),
                                Organization_Id = reader.GetString(reader.GetOrdinal("Organization_Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Website = reader.GetString(reader.GetOrdinal("Website")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Founded = reader.GetInt32(reader.GetOrdinal("Founded")),
                                Industry = reader.GetString(reader.GetOrdinal("Industry")),
                                NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees")),
                                CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"))
                            };
                        }
                    }
                }
            }

            return organization;
        }
        public int CountCountries(string connectionString)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Countries WHERE IsDeleted = 0;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }
        public int CountOrganizations(string connectionString)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Organizations WHERE IsDeleted = 0;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }
    }
}
