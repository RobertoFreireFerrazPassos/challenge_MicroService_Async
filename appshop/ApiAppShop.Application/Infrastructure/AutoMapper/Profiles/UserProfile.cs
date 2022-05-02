using ApiAppShop.Application.DataContracts.Requests.User;
using ApiAppShop.Application.DataContracts.Responses.User;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Entities;
using AutoMapper;

namespace ApiAppShop.Application.Infrastructure.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity,UserDto>()
                .ForMember(x => x.Password, opt => opt.Ignore());

            CreateMap<UserDto, UserEntity>();

            CreateMap<SignInRequest, UserDto>();

            CreateMap<UserDto, LogInResponse>(); 
            
            CreateMap<LogInRequest, LogInDto>();
        }
    }
}
