using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mosef_Server
{
    internal class User
    {
        public string? UserId { get; set; }
        public string? UserSSN { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UserPhone { get; set; }
        public string? UserLocation { get; set; }
        public string? UserGender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? UserStatus { get; set; }

        private TcpClient? client;
        List<string> UserServiceHistory = new List<string>();

        public string UserData()
        {
            return $"{UserSSN}${UserFirstName}${UserLastName}${UserEmail}${UserPhone}${UserLocation}${UserGender}${BirthDate}${UserStatus}";
        }

        public string UserServices()
        {
            string services = "";
            foreach (string service in UserServiceHistory)
            {
                services += service + "$";
            }
            return services;
        }
    }
}
