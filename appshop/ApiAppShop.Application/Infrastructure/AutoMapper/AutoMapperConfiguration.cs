﻿using AutoMapper;
using ApiAppShop.Application.Infrastructure.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ApiAppShop.Application.Infrastructure.AutoMapper
{
	public static class AutoMapperConfiguration
	{
        public static void RegisterMappings(IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppProfile());
            });


            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}