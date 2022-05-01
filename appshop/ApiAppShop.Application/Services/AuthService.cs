using ApiAppShop.Application.Infrastructure.Security;
using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Enums;
using ApiAppShop.Domain.Services;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace ApiAppShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserDomainService _userDomainService;

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public AuthService(IUserDomainService userDomainService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userDomainService = userDomainService ??
                throw new ArgumentNullException(nameof(userDomainService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        public string LogIn(LogInDto logInInfo)
        {
            var user = _mapper.Map<UserDto>(
                    _userDomainService.GetUserByName(logInInfo.Name) ??
                    throw new Exception(ErrorMessageConstants.USER_DOESNT_EXIST)
                );

            var isValidPassword = Hash.Verify(
                logInInfo.Password,
                user.PasswordHash,
                user.PasswordSalt);

            if (!isValidPassword) throw new Exception(ErrorMessageConstants.USER_NOT_AUTHENTICATED);

            user.PasswordSalt = default(Byte[]);

            user.PasswordHash = default(Byte[]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            return Token.Create(user, key);
        }

        public void CreateNewUser(UserDto user)
        {
            Hash.Create(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userEntity = new UserEntity()
            {
                Name = user.Name,
                Cpf = user.Cpf,
                Role = UserRoleEnum.Guest,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                CreditCard = user.CreditCard,
                Address = user.Address
            };

            _userDomainService.CreateNewUser(userEntity);
        }
    }
}
