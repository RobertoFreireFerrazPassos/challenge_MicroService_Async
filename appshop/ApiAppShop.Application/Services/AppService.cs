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
        private readonly IAppsByUserRepository _appsByUserRepository;
        private readonly IMapper _mapper;

        private readonly string appbyuser = CacheKeyPrefixConstants.APP_BY_USER_;

        public AppService(ICache cache,
            IAppRepository appRepository,
            IAppsByUserRepository appsByUserRepository,
            IMapper mapper)
        {
            _cache = cache ??
                throw new ArgumentNullException(nameof(cache));
            _appRepository = appRepository ??
                throw new ArgumentNullException(nameof(appRepository));
            _appsByUserRepository = appsByUserRepository ??
                throw new ArgumentNullException(nameof(appsByUserRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public void AddApp(AppDto addApp)
        {
            _appRepository.SetApp(_mapper.Map<AppEntity>(addApp));
        }

        public IEnumerable<AppDto> GetApps()
        {
            var apps = _mapper.Map<IEnumerable<AppDto>>(_appRepository.GetApps());
            return apps;
        }

        public AppDto GetApp(string appId)
        {
            var app = _appRepository.GetApp(appId);
            if (app == null) throw new Exception(ErrorMessageConstants.APP_DOESNT_EXIST);
            return _mapper.Map<AppDto>(app);
        }

        public bool ValidateApp(string appId)
        {
            var app = _appRepository.GetApp(appId);
            if (app == null) throw new Exception(ErrorMessageConstants.APP_DOESNT_EXIST);
            return true;
        }

        public void AddAppByUser(AppPurchasedDto appPurchased)
        {
            var userId = appPurchased.UserId;
            bool areAppsbyUserFromDatabase = false;
            var apps = GetAppsByUser(userId, out areAppsbyUserFromDatabase);
            var newPurchaseApp = GetApp(appPurchased.AppId);

            if (apps.Find(a => a.Id == newPurchaseApp.Id) == null) apps.Add(newPurchaseApp);

            SetAppsByUserInDatabase(userId, apps, areAppsbyUserFromDatabase);
            SetAppsByUserInCache(userId, apps);
        }

        private void SetAppsByUserInDatabase(string userId, IEnumerable<AppDto> apps, bool areAppsbyUserFromDatabase)
        {
            var appsByUserEntity = new AppsByUserDto()
            {
                UserId = userId,
                Apps = _mapper.Map<IEnumerable<AppDto>>(apps)
            };

            if (areAppsbyUserFromDatabase)
                ReplaceAppsByUser(appsByUserEntity);            
            else
                SaveAppsByUser(appsByUserEntity);
        }

        private void SaveAppsByUser(AppsByUserDto appsByUser)
        {
            _appsByUserRepository.SetAppsByUser(ConvertAppsByUserDtoToEntity(appsByUser));
        }

        private void ReplaceAppsByUser(AppsByUserDto appsByUser)
        {
            _appsByUserRepository.ReplaceUser(ConvertAppsByUserDtoToEntity(appsByUser));
        }
        private AppsByUserEntity ConvertAppsByUserDtoToEntity(AppsByUserDto appsByUser)
        {
            return _mapper.Map<AppsByUserEntity>(appsByUser);
        }

        public List<AppDto> GetAppsByUser(string userId, out bool areAppsbyUserFromDatabase)
        {
            var apps = GetAppsByUserInCache(userId).ToList();
            areAppsbyUserFromDatabase = false;

            if (apps.Count == 0)
            {
                var appsByUser = GetAppsByUserInDatabase(userId);

                if (appsByUser != null) {
                    areAppsbyUserFromDatabase = true;
                    apps = _mapper.Map<IEnumerable<AppDto>>(appsByUser.Apps).ToList();
                } 
            }
            return apps;
        }

        private IEnumerable<AppDto> GetAppsByUserInCache(string userId)
        {
            var result = _cache.Get(BuildKey(userId));
            if (result == null) return new List<AppDto>();
            return JsonSerializer.Deserialize<IEnumerable<AppDto>>(result);
        }

        private AppsByUserEntity GetAppsByUserInDatabase(string userId)
        {
            return _appsByUserRepository.GetAppsByUserId(userId);
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
