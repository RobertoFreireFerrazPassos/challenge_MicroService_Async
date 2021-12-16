using System.Collections.Generic;
using System.Threading.Tasks;
using ApiUser.Domain.Services.User;
using ApiUser.Domain.Dtos.User;
using ApiUser.Domain.Repositories;
using System;
using AutoMapper;
using ApiUser.Domain.Entities;

namespace UserRegistration.Application.Services.User
{
    public class UserService : IUserService
    {
        public IMapper Mapper { get; }
        public IUserRepository UserRepository { get; }

        public UserService(
            IMapper mapper,
            IUserRepository userRepository) 
        
        {
            Mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            UserRepository = userRepository ??
              throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var result = await UserRepository.GetUsersAsync();
            return Mapper.Map<IEnumerable<UserDto>>(result);
        }
        
        public async Task<IEnumerable<UserDto>> GetUsersByFilterAsync(UserFilterDto userfilter)
        {
            var result = await UserRepository.GetUsersAsync(userfilter);
            return Mapper.Map<IEnumerable<UserDto>>(result);
        }

        public async Task<int> ToggleActivationInUserAsync(UserToggleActivationDto userToggleActivationDto)
        {
            return await UserRepository.ToggleActivationInUserAsync(userToggleActivationDto);
        }

        public async Task<int> EditUserAsync(UserDto user) 
        {
            return await UserRepository.EditUserAsync(Mapper.Map<UserEntity>(user));
        }

        public async Task<bool> AddUserAsync(UserDto user)
        {
            return await UserRepository.AddUserAsync(Mapper.Map<UserEntity>(user));
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await UserRepository.DeleteUserAsync(id);
        }
    }
}
