using ApiDatabaseServices.Interfaces;
using ApiDatabaseServices.ViewModels;
using ApiServices.Interfaces;
using CsvReaderAPI.Services.Interfaces;
using CsvReaderAPI.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Implementation
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthorizationService _authorizationService;
        public AuthenticationService(IDatabaseService databaseService, IPasswordHasher passwordHasher,IAuthorizationService service)
        {
            _databaseService = databaseService;
            _passwordHasher = passwordHasher;
            _authorizationService = service;
        }
        public int Register(RegisterViewModel model)
        {
            var accounts = _databaseService.SelectData<Account>("Accounts");
            if (accounts.Any(acc => acc.Username == model.Username))
            {
                return (int)HttpStatusCode.BadRequest;
            }
            var salt = _passwordHasher.GenerateSalt();
            string hashPass = _passwordHasher.HashPassword(model.Password, salt);
            var newAccount = new Account(model.Username, hashPass, model.Email, Convert.ToBase64String(salt));
            _databaseService.InsertData(newAccount);
            UserViewModel user = new UserViewModel(newAccount.Username, newAccount.Email,newAccount.UserType);
            return (int)HttpStatusCode.Created;
        }
        public UserViewModel Login(string email, string password)
        {
            try
            {
                var accs = _databaseService.SelectData<Account>("Accounts");
                var currentAcc = accs.FirstOrDefault((acc) => acc.Email == email);
                var salt = Convert.FromBase64String(currentAcc.Salt);
                if (_passwordHasher.VerifyPassword(password, currentAcc.Password, salt))
                {
                    string token = _authorizationService.GenerateJwtToken(currentAcc.Username);
                    _authorizationService.SaveTokenInSessionStorage(token);
                    UserViewModel user = new UserViewModel(currentAcc.Username, currentAcc.Email, currentAcc.UserType);
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CheckToken(string token)
        {
            if (_authorizationService.CheckToken(token))
            {
                return (int)HttpStatusCode.OK;
            }
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
