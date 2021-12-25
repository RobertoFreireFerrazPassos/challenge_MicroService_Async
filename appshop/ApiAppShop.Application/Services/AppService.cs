using ApiAppShop.Application.DataContracts.Requests.App;
using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ApiAppShop.Application.Services
{
    public class AppService : IAppService
    {
        private readonly ICache _cache;
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;

        private readonly string appbyuser = CacheKeyPrefixConstants.APP_BY_USER_;

        public AppService(ICache cache,
            IAppRepository appRepository,
            IMapper mapper)
        {
            _cache = cache ??
                throw new ArgumentNullException(nameof(cache));
            _appRepository = appRepository ??
                throw new ArgumentNullException(nameof(appRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public void AddApp(AppDto addApp)
        {
            addApp.Id = Guid.NewGuid().ToString();
            _appRepository.SetApp(_mapper.Map<AppEntity>(addApp));
        }

        public IEnumerable<AppDto> GetApps()
        {
            var apps = _mapper.Map<IEnumerable<AppDto>>(_appRepository.GetApps());
            return apps;
        }

        public AppDto GetApp(string appId)
        {
            return _mapper.Map<AppDto>(_appRepository.GetApp(appId));
        }

        public void AddAppByUser(AppPurchasedDto appPurchased)
        {
            var userId = appPurchased.UserId;
            var Apps = GetAppsByUserInCache(userId).ToList();
            var newPurchaseApp = GetApp(appPurchased.AppId);

            if (Apps.Find(a => a.Id == newPurchaseApp.Id) == null) Apps.Add(newPurchaseApp);

            SetAppsByUserInCache(userId, Apps);
        }

        public IEnumerable<AppDto> GetAppsByUser(string userId)
        {
            return GetAppsByUserInCache(userId);
        }

        private IEnumerable<AppDto> GetAppsByUserInCache(string userId)
        {
            var result = _cache.Get(BuildKey(userId));
            if (result == null) return new List<AppDto>();
            return JsonSerializer.Deserialize<IEnumerable<AppDto>>(result);
        }

        private void SetAppsByUserInCache(string userId, IEnumerable<AppDto> apps)
        {
            string value = JsonSerializer.Serialize(apps);
            _cache.Set(BuildKey(userId), value);
        }

        private string BuildKey(string key) {
            return appbyuser + key;
        }
    }
}
