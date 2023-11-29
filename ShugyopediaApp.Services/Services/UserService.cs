using ShugyopediaApp.Data;
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
using ShugyopediaApp.Data.Repositories;

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

        public void AddUser(UserViewModel model)
        {
            var user = new User();
            if (!_repository.UserExists(model.UserId))
            {
                _mapper.Map(model, user);
                user.Password = PasswordManager.EncryptPassword(model.Password);
                user.CreatedTime = DateTime.Now;
                user.UpdatedTime = DateTime.Now;
                user.CreatedBy = System.Environment.UserName;
                user.UpdatedBy = System.Environment.UserName;

                _repository.AddUser(user);
            }
            else
            {
                throw new InvalidDataException(Resources.Messages.Errors.UserExists);
            }
        }

        public void DeleteUser(int id)
        {
            var model = new User();
            model.Id = id;
            _userRepository.DeleteUser(model);
        }
    }
}
