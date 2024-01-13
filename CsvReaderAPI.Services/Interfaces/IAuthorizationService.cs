using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderAPI.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public string GenerateJwtToken(string username);
        public void SaveTokenInSessionStorage(string token);
        public string GetTokenFromSessionStorage(string userId);
        public bool CheckToken(string token);
    }
}
