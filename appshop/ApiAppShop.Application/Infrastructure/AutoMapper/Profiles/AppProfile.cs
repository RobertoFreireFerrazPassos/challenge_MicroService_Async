using ApiAppShop.Domain.Dtos;
using ApiUser.Application.DataContracts;
using AutoMapper;
using System.Collections.Generic;

namespace ApiAppShop.Application.Infrastructure.AutoMapper.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<AppCreationRequest, AppCreationDto>();
            CreateMap<AppDto,App>();
        }
    }
}
