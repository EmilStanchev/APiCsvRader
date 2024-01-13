using CsvReaderAPI.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public int Register(string username, string password, string email);
        public UserViewModel Login(string email, string password);
        public int CheckToken(string token);
    }
}
