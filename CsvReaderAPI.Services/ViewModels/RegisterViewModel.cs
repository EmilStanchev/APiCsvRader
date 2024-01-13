using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public RegisterViewModel(string username,string password,string email) 
        { 
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
