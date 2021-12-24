using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Cache.Items;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Services;
using System.Collections.Generic;

namespace ApiAppShop.Application.Services
{
    public class AppService : IAppService
    {
        private readonly ICache _cache;
        private readonly string appbyuser ="appbyuser_";

        public AppService(ICache cache)
        {
            _cache = cache;
        }

        public void SetItem(AppCreationDto appCreationRequest)
        {
            string key = appbyuser + appCreationRequest.UserId;
            AppItem value = new AppItem
            {
                Name = appCreationRequest.Name,
                Price = appCreationRequest.Price
            };

            _cache.Set(KeyValuePair.Create("chave", "valor"));

            //_cache.Set(KeyValuePair.Create(key, value.ToString()));
        }

        public IEnumerable<AppDto> GetItems()
        {
            return null;
        }

        public IEnumerable<AppDto> GetItem(string key)
        {
            var result = _cache.Get("chave");
            //key = appbyuser + key;
            //var apps = _cache.Get(key);
            return null;
        }
    }
}
