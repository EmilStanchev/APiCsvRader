using ApiDatabaseServices.ViewModels;
using ApiServices.Interfaces;
using CsvReaderAPI.Services.Interfaces;
using CsvReaderAPI.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Implementation
{
    public class AccountService:IAccountService
    {
        private readonly IDatabaseService _database;
        public AccountService(IDatabaseService database)
        {
            _database = database;
        }
        public List<UserViewModel> GetAllAccount()
        {
            var accounts = _database.SelectData<Account>("Accounts");
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var account in accounts)
            {
                result.Add(new UserViewModel(account.Username, account.Email, account.UserType));
            }
            return result;
        }
        public int DeleteAccount(string deleteAccountId, string accountId)
        {
            var account = _database.GetEntityById<Account>(accountId, "Accounts", "Id");
            if (account.UserType == "Admin")
            {
                try
                {

                    _database.SoftDeleteAccount(deleteAccountId);
                    return StatusCodes.Status200OK;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCodes.Status400BadRequest;
                }
            }
            return StatusCodes.Status401Unauthorized;
        }
    }
}
