using ApiAppShop.Domain.Cache.Items.Base;

namespace ApiAppShop.Domain.Cache.Items
{
    public class AppItem : CacheItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
