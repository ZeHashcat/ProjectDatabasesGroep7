using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class User
    {
        public string Username { get; set; }
        public HashWithSaltResult Password { get; set; }
        public string SecretQuestion { get; set; }
        public HashWithSaltResult SecretAnswer { get; set; }
    }
}
