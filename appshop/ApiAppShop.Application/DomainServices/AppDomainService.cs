using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;

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

        public void AddApp(AppEntity app)
        {
            _appRepository.SetApp(app);
        }

        public IEnumerable<AppEntity> GetApps()
        {
            return _appRepository.GetApps();
        }

        public AppEntity GetApp(string appId)
        {
            return _appRepository.GetApp(appId);
        }
    }
}
