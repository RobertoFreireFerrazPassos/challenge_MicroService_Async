using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ApiAppShop.CrossCutting.IoC
{
    public class RegisterRedisCacheService
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = "RedisCacheInstance";
            });
        }
    }
}
