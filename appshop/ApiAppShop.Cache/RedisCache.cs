using ApiAppShop.Domain.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;

namespace ApiAppShop.Cache
{
    public class RedisCache : ICache
    {
        private readonly IDistributedCache _database;

        public RedisCache(IDistributedCache database)
        {
            _database = database;
        }

        public string Get(string key)
        {
            return _database.GetString(key);
        }

        public void Set(KeyValuePair<string, string> keyValue)
        {
            _database.SetString(keyValue.Key, keyValue.Value);
        }
    }
}
