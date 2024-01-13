using ApiDatabaseServices.ViewModels;
using ApiServices.Interfaces;
using CsvReaderAPI.Services.Interfaces;
using CsvReaderAPI.Services.ViewModels;
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
    }
}
