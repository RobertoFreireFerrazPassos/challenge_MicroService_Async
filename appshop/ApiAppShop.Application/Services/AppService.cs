using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace ApiAppShop.Application.Services
{
    public class AppService : IAppService
    {
        private readonly IAppDomainService _appDomainService;

        private readonly IMapper _mapper;

        public AppService(
            IAppDomainService appDomainService,
            IMapper mapper)
        {
            _appDomainService = appDomainService ??
                throw new ArgumentNullException(nameof(appDomainService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));            
        }

        public void AddApp(AppDto addApp)
        {
            _appDomainService.AddApp(_mapper.Map<AppEntity>(addApp));
        }

        public IEnumerable<AppDto> GetApps()
        {
            var apps = _mapper.Map<IEnumerable<AppDto>>(_appDomainService.GetApps());

            return apps;
        }

        public AppDto GetApp(string appId)
        {
            var app = _appDomainService.GetApp(appId);

            if (app == null) throw new Exception(ErrorMessageConstants.APP_DOESNT_EXIST);

            return _mapper.Map<AppDto>(app);
        }
    }
}
