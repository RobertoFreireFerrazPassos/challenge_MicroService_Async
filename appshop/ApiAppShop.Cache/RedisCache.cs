using ApiAppShop.Domain.Cache;
using StackExchange.Redis;
using System.Collections.Generic;

namespace ApiAppShop.Cache
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;

        public RedisCache(IDatabase database)
        {
            _database = database;
        }

        public string Get(string key)
        {
            return _database.StringGet(key);
        }

        public void Set(KeyValuePair<string, string> keyValue)
        {
            _database.StringSet(keyValue.Key, keyValue.Value);
        }
    }
}
