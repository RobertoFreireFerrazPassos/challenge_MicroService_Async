using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ApiAppShop.Application.Services
{
    public class AppService : IAppService
    {
        private readonly ICache _cache;
        private readonly string appbyuser ="appsbyuser_";

        public AppService(ICache cache)
        {
            _cache = cache;
        }

        public void AddAppByUser(AppCreationDto appCreationRequest)
        {
            var userId = appCreationRequest.UserId;
            var Apps = GetAppsByUserInCache(userId).ToList();
            var newApp = new AppDto
            {
                Name = appCreationRequest.Name,
                Price = appCreationRequest.Price
            };

            if (Apps.Find(a => a.Name == newApp.Name) == null) Apps.Add(newApp);

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
