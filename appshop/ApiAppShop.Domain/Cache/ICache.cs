using System.Collections.Generic;

namespace ApiAppShop.Domain.Cache
{
    public interface ICache
    {
        public string Get(string key);

        public void Set(KeyValuePair<string, string> keyValue);
    }
}
