using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Interfaces
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password, byte[] salt);
        public bool VerifyPassword(string password, string hash, byte[] salt);
        public byte[] GenerateSalt();
    }
}
