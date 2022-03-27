using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class HashWithSaltResult
    {
        public string Hash { get; set; }
        public string Salt { get; set; }

        public HashWithSaltResult(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}
