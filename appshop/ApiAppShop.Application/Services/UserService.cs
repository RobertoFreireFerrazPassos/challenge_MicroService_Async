using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;
using System.Threading.Tasks;

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

        public async Task<UserDto> GetUserAsync(string userId)
        {
            var user = await _userDomainService.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new Exception(ErrorMessageConstants.USER_DOESNT_EXIST);
            }

            user.PasswordSalt = default(byte[]);

            user.PasswordHash = default(byte[]);

            return _mapper.Map<UserDto>(user);
        }

        public async Task SetUserAsync(UserDto user)
        {
            await _userDomainService.UpdateUserAsync(_mapper.Map<UserEntity>(user));
        }
    }
}
