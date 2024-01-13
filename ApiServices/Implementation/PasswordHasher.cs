using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiDatabaseServices.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApiDatabaseServices.Implementation
{
    public class PasswordHasher:IPasswordHasher
    {
        private readonly IConfiguration  _configuration;
        private readonly int _keySize;
        private readonly int _iterations;
        private readonly HashAlgorithmName _hashAlgorithm;
        public PasswordHasher(IConfiguration configuration)
        {
            _configuration = configuration;
            _keySize = int.Parse(_configuration.GetSection("AppSettings").GetSection("keySize").Value);
            _iterations = int.Parse(_configuration.GetSection("AppSettings").GetSection("iterations").Value);
            _hashAlgorithm = HashAlgorithmName.SHA512;
        }

        public string HashPassword(string password, byte[] salt)
        {
            return Hashing(password, salt);
        }
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[_keySize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private string Hashing(string password, byte[] salt)
        {
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);
            return Convert.ToHexString(hash);
        }
    }
}
