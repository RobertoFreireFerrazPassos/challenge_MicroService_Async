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
            CreateMap<IEnumerable<AppDto>, AppResponse>()
                .ConvertUsing(source => new AppResponse
                    {
                        Apps = (IEnumerable<App>)source
                    }
                );
        }
    }
}
