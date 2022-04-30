using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using AutoMapper;
using System;

namespace ApiAppShop.Application.DomainServices
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserDomainService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public UserEntity GetUser(string userId)
        {
            return _userRepository.GetUser(userId);
        }

        public void SetUser(UserDto user)
        {
            string userId = user.Id;

            if (IsNewUser(userId))
            {
                _userRepository.SetUser(_mapper.Map<UserEntity>(user));
            }
            else
            {
                _userRepository.ReplaceUser(_mapper.Map<UserEntity>(user));
            }

            bool IsNewUser(string userId)
            {
                return string.IsNullOrEmpty(userId) || GetUser(userId) is null;
            }
        }
    }
}
