using ApiAppShop.Application.DataContracts.Requests.App;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Application.DataContracts;
using AutoMapper;

namespace ApiAppShop.Application.Infrastructure.AutoMapper.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<AppDto,App>();

            CreateMap<AddAppRequest, AppDto>();

            CreateMap<AppEntity, AppDto>().ReverseMap();

            CreateMap<UserAccountEntity, UserAccountDto>().ReverseMap();            
        }
    }
}
