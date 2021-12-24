using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ApiAppShop.CrossCutting.IoC
{
    public class RegisterRedisCacheService
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
            services.AddScoped(s => redis.GetDatabase());
        }
    }
}
