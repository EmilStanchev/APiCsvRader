using CsvReaderAPI.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Interfaces
{
    public interface IAccountService
    {
        public List<UserViewModel> GetAllAccount();
        public int DeleteAccount(string deleteAccountId, string accountId);
        public int ChangeRole(string changingAccountId, string accountId, string userType);
    }
}
