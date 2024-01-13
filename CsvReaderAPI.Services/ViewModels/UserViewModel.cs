using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string JwtToken { get; set; }
        public UserViewModel(string username, string email, string userType)
        {
            Username = username;
            Email = email;
            UserType = userType;
        }

    }
}
