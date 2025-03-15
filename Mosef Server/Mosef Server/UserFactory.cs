using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosef_Server
{
    internal static class UserFactory
    {
        public static User CreateUser(
        string userSSN, string userFirstName, string userLastName,
        string userEmail, string userPassword, string userPhone, string userLocation,
        string? userGender, DateTime? birthDate, string? userStatus, GeneratorId generatorId)
        {
            return new User
            {
                UserId = generatorId.GenerateRandomId(),
                UserSSN = userSSN,
                UserFirstName = userFirstName,
                UserLastName = userLastName,
                UserEmail = userEmail,
                UserPassword = userPassword,
                UserPhone = userPhone,
                UserLocation = userLocation,
                UserGender = userGender,
                BirthDate = birthDate,
                UserStatus = userStatus
            };
        }
    }
}
