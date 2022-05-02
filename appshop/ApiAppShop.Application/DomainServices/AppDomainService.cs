using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Application.DomainServices
{
    public class AppDomainService : IAppDomainService
    {
        private readonly IAppRepository _appRepository;

        public AppDomainService(IAppRepository appRepository)
        {
            _appRepository = appRepository ??
                throw new ArgumentNullException(nameof(appRepository));
        }

        public async Task AddAppAsync(AppEntity app)
        {
            await _appRepository.SetAppAsync(app);
        }

        public async Task<IEnumerable<AppEntity>> GetAppsAsync()
        {
            return await _appRepository.GetAppsAsync();
        }

        public async Task<AppEntity> GetAppAsync(string appId)
        {
            return await _appRepository.GetAppAsync(appId);
        }
    }
}
