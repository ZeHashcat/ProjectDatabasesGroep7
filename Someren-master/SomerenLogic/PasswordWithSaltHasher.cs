using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SomerenModel;

namespace SomerenLogic
{
    public class PasswordWithSaltHasher
    {
        public HashWithSaltResult HashWithSalt(string password, int saltLength, HashAlgorithm hashAlgorithm)
        {
            RandomNumberGeneratorHashSalt rng = new RandomNumberGeneratorHashSalt();
            byte[] saltBytes = rng.GeneratRandomCryptographicBytes(saltLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();

            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);

            byte[] hashBytes = hashAlgorithm.ComputeHash(passwordWithSaltBytes.ToArray());

            return new HashWithSaltResult(Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }
        public HashWithSaltResult HashWithSalt(string password, byte[] saltBytes, HashAlgorithm hashAlgorithm)
        {
            RandomNumberGeneratorHashSalt rng = new RandomNumberGeneratorHashSalt();
            //byte[] saltBytes = rng.GeneratRandomCryptographicBytes(saltLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();

            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);

            byte[] hashBytes = hashAlgorithm.ComputeHash(passwordWithSaltBytes.ToArray());

            return new HashWithSaltResult(Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }
    }
}
