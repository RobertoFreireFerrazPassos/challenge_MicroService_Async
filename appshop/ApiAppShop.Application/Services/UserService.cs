using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;

namespace ApiAppShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDomainService _userDomainService;
        private readonly IMapper _mapper;

        public UserService(IUserDomainService userDomainService,
            IMapper mapper)
        {
            _userDomainService = userDomainService ??
                throw new ArgumentNullException(nameof(userDomainService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public UserDto GetUser(string userId)
        {
            var user = _userDomainService.GetUser(userId);

            if (user == null)
            {
                throw new Exception(ErrorMessageConstants.USER_DOESNT_EXIST);
            }

            return _mapper.Map<UserDto>(user);
        }

        public void SetUser(UserDto user)
        {
            _userDomainService.SetUser(user);
        }
    }
}
