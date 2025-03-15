using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosef_Server
{
    internal class DataBase
    {
        Dictionary<string, User> UsersData = new Dictionary<string, User>();

        public User? GetUserData(string UserEmail)
        {
            if (UsersData.ContainsKey(UserEmail))
            {
                return UsersData[UserEmail];
            }
            else
            {
                return null;
            }
        }
        public void AddUserData(User user)
        {
            if (user.UserEmail != null)
            {
                UsersData.Add(user.UserEmail, user);
            }
            else
            {
                throw new ArgumentNullException(nameof(user.UserEmail), "User email cannot be null");
            }
        }
        public void DeleteUserData(string UserEmail)
        {
            if (UsersData.ContainsKey(UserEmail))
            {
                UsersData.Remove(UserEmail);
            }
            else
            {
                throw new ArgumentNullException(nameof(UserEmail), "User email does not exist");
            }
        }
        public void UpdateUserData(User user)
        {
            if (user.UserEmail != null && UsersData.ContainsKey(user.UserEmail))
            {
                UsersData[user.UserEmail] = user;
            }
            else
            {
                throw new ArgumentNullException(nameof(user.UserEmail), "User email does not exist");
            }
        }
    }
}
