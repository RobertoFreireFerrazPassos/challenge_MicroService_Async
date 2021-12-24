using System.Text.Json;

namespace ApiAppShop.Domain.Cache.Items.Base
{
    public class CacheItem
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
