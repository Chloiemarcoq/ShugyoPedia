﻿using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        User GetUserByEmail(string email);
        bool UserExists(string userId);
        bool UserExistsEmail(string userEmail);
        void AddUser(User user);
        void DeleteUser(User user);
        void EditUser(User user);
        void ResetPassword(User user);
    }
}
