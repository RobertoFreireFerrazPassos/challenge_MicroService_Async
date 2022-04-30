using ApiAppShop.Application.DataContracts.Requests.App;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiUser.Application.DataContracts;
using AutoMapper;
using System.Collections.Generic;

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
