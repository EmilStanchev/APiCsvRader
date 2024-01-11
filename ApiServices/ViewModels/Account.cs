using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.ViewModels
{
        public class Account:BaseModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string UserType { get; set; }
            public string Salt { get; set; }

            public Account()
            {
            }
            public Account(string username, string password, string email, string salt)
            {
                Username = username;
                Password = password;
                Email = email;
                Salt = salt;
                UserType = "Normal";
            }
    }
}
