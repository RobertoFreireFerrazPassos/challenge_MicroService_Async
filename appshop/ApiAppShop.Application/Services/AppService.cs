using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task AddAppAsync(AppDto addApp)
        {
            await _appDomainService.AddAppAsync(_mapper.Map<AppEntity>(addApp));
        }

        public async Task<IEnumerable<AppDto>> GetAppsAsync()
        {
            var apps = _mapper.Map<IEnumerable<AppDto>>(await _appDomainService.GetAppsAsync());

            return apps;
        }

        public async Task<AppDto> GetAppAsync(string appId)
        {
            var app = await _appDomainService.GetAppAsync(appId);

            if (app == null) throw new Exception(ErrorMessageConstants.APP_DOESNT_EXIST);

            return _mapper.Map<AppDto>(app);
        }
    }
}
