using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.ServiceModels;
using System.Collections.Generic;
using static ShugyopediaApp.Resources.Constants.Enums;

namespace ShugyopediaApp.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult AuthenticateUser(string userid, string password, ref User user);
        List<UserViewModel> GetUsers();
        User GetUserByEmail(string email);
        public void AddUser(UserViewModel addUser, string user);
        void EditUser(UserViewModel editUser, string user);
        void DeleteUser(int id);
        bool UserExistsEmail(string email);
        void ResetPassword(UserViewModel user);
    }
}
