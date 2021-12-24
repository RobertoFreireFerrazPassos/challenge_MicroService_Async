using Microsoft.Extensions.DependencyInjection;

namespace ApiAppShop.CrossCutting.IoC
{
    public class RegisterRedisCacheService
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = "RedisCacheInstance";
            });
        }
    }
}
