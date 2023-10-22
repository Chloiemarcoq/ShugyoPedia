using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.ServiceModels;
using static ShugyopediaApp.Resources.Constants.Enums;

namespace ShugyopediaApp.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult AuthenticateUser(string userid, string password, ref User user);
        void AddUser(UserViewModel model);
    }
}
