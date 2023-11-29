﻿using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {

        }

        public IQueryable<User> GetUsers()
        {
            return this.GetDbSet<User>();
        }

        public bool UserExists(string userId)
        {
            return this.GetDbSet<User>().Any(x => x.UserId == userId);
        }

        public void AddUser(User user)
        {
            this.GetDbSet<User>().Add(user);
            UnitOfWork.SaveChanges();
        }
        public void EditUser(User user)
        {
            var recordFound = this.GetDbSet<User>().Find(user.Id);
            if (recordFound != null)
            {
                recordFound.UserId = user.UserId;
                recordFound.Name = user.Name;
                recordFound.UserEmail = user.UserEmail;
                recordFound.Password = user.Password;
                recordFound.UpdatedBy = user.UpdatedBy;
                recordFound.UpdatedTime = user.UpdatedTime;
                UnitOfWork.SaveChanges();
            }
        }

    }
}
