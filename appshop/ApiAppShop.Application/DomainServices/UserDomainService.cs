using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using System;
using System.Threading.Tasks;

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

        public async Task<UserEntity> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

        public async Task<UserEntity> GetUserByNameAsync(string name)
        {
            return await _userRepository.GetUserByNameAsync(name);
        }        

        public async Task CreateNewUserAsync(UserEntity user)
        {
            if (!IsNewUser(user.Id))
            {
                throw new Exception(String.Format(ErrorMessageConstants.NOT_A_NEW_USER,user.Id));
            }

            await _userRepository.SetUserAsync(user);

            bool IsNewUser(string userId)
            {
                return string.IsNullOrEmpty(userId);
            }
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            await _userRepository.ReplaceUserAsync(user);
        }
    }
}
