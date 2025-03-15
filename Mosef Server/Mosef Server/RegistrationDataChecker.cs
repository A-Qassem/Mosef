using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosef_Server
{
    internal class RegistrationDataChecker
    {
        public bool EmailChecker(string email)
        {
            return (email.EndsWith("@gmail.com") && !email.Contains("@@") ? true : false);
        }
        public bool PasswordChecker(string email, string password, DataBase dataBase)
        {
            return (dataBase.GetUserData(email)?.UserPassword == password ? true : false);
        }
    }
}
