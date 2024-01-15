using ApiDatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Implementation
{
    public class DeleteService:IDeleteService
    {
        public void SoftDeleteCountry(int countryId, string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string countryQuery = @"
                        UPDATE Countries
                        SET IsDeleted = 1
                        WHERE Id = @CountryId;
                    ";

                        using (SQLiteCommand countryCommand = new SQLiteCommand(countryQuery, connection, transaction))
                        {
                            countryCommand.Parameters.AddWithValue("@CountryId", countryId);
                            countryCommand.ExecuteNonQuery();
                        }
                        string organizationsQuery = @"
                        UPDATE Organizations
                        SET IsDeleted = 1
                        WHERE CountryId = @CountryId;
                    ";

                        using (SQLiteCommand organizationsCommand = new SQLiteCommand(organizationsQuery, connection, transaction))
                        {
                            organizationsCommand.Parameters.AddWithValue("@CountryId", countryId);
                            organizationsCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void SoftDeleteOrganization(string organizationId, string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string organizationQuery = @"
                    UPDATE Organizations
                    SET IsDeleted = 1
                    WHERE Organization_Id = @OrganizationId";

                        using (SQLiteCommand organizationCommand = new SQLiteCommand(organizationQuery, connection, transaction))
                        {
                            organizationCommand.Parameters.Add(new SQLiteParameter("@OrganizationId", organizationId));
                            organizationCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
        }
        public void SoftDeleteAccount(string accountId, string connectionString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string accountQuery = @"
                    UPDATE Accounts
                    SET IsDeleted = 1
                    WHERE Id = @AccountId";

                        using (SQLiteCommand accountCommand = new SQLiteCommand(accountQuery, connection, transaction))
                        {
                            accountCommand.Parameters.Add(new SQLiteParameter("@AccountId", accountId));
                            accountCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
