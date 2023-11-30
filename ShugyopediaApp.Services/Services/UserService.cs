using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.Manager;
using ShugyopediaApp.Services.ServiceModels;
using AutoMapper;
using System;
using System.IO;
using System.Linq;
using static ShugyopediaApp.Resources.Constants.Enums;
using System.Collections.Generic;
using ShugyopediaApp.Data.Repositories;
using ShugyopediaApp.Data;
using System.Text.RegularExpressions;

namespace ShugyopediaApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public LoginResult AuthenticateUser(string userId, string password, ref User user)
        {
            user = new User();
            var passwordKey = PasswordManager.EncryptPassword(password);
            user = _repository.GetUsers().Where(x => x.UserId == userId &&
                                                     x.Password == passwordKey).FirstOrDefault();

            return user != null ? LoginResult.Success : LoginResult.Failed;
        }
        public List<UserViewModel> GetUsers()
        {
            List<UserViewModel> users = _repository.GetUsers()
                .Select(s => new UserViewModel
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Name = s.Name,
                    UserEmail = s.UserEmail,
                    Password = PasswordManager.DecryptPassword(s.Password)
                })
                .OrderBy(s => s.UserId)
                .ToList();
            return users;
        }
        public User GetUserByEmail(string email)
        {
            User user = _repository.GetUserByEmail(email);
            return user;
        }

        public void AddUser(UserViewModel addUser, string user)
        {
            if (!_repository.UserExists(addUser.UserId))
            {
                User model = new User();
                model.UserId = addUser.UserId;
                model.Name = addUser.Name;
                model.UserEmail = addUser.UserEmail;
                model.Password = PasswordManager.EncryptPassword(addUser.Password);
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                model.CreatedBy = user;
                model.UpdatedBy = user;
                _repository.AddUser(model);
            }
            else
            {
                throw new InvalidDataException(Resources.Messages.Errors.UserExists);
            }
        }
        public void EditUser(UserViewModel editUser, string user)
        {
            User model = new User();
            model.Id = editUser.Id;
            model.UserId = editUser.UserId;
            model.Name = editUser.Name;
            model.UserEmail = editUser.UserEmail;
            model.Password = PasswordManager.EncryptPassword(editUser.Password);
            model.UpdatedTime = DateTime.Now;
            model.UpdatedBy = user;
            _repository.EditUser(model);
        }
        public bool UserExistsEmail(string email)
        {
            return _repository.UserExistsEmail(email);
        }
        public void ResetPassword(UserViewModel user)
        {
            User model = new User();
            model.UserEmail = user.UserEmail;
            model.Password = PasswordManager.EncryptPassword(user.Password);
            model.UpdatedTime = DateTime.Now;
            model.UpdatedBy = user.UserEmail;
            _repository.ResetPassword(model);
        }
    }
}
