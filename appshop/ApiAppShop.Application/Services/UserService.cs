using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;

namespace ApiAppShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public UserDto GetUser(string userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null) throw new Exception(ErrorMessageConstants.USER_DOESNT_EXIST);
            return _mapper.Map<UserDto>(user);
        }

        public void SetUser(UserDto user)
        {
            _userRepository.SetUser(_mapper.Map<UserEntity>(user));
        }
    }
}
