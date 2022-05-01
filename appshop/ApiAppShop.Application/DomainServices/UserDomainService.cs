using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using System;

namespace ApiAppShop.Application.DomainServices
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public UserEntity GetUserById(string userId)
        {
            return _userRepository.GetUser(userId);
        }

        public UserEntity GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }        

        public void CreateNewUser(UserEntity user)
        {
            if (!IsNewUser(user.Id))
            {
                throw new Exception(String.Format(ErrorMessageConstants.NOT_A_NEW_USER,user.Id));
            }

            _userRepository.SetUser(user);

            bool IsNewUser(string userId)
            {
                return string.IsNullOrEmpty(userId);
            }
        }

        public void UpdateUser(UserEntity user)
        {
            _userRepository.ReplaceUser(user);
        }
    }
}
