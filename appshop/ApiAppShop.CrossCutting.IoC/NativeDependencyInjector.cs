using ApiAppShop.Application.Services;
using ApiAppShop.DataAccess.Repositories;
using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Events.Producers;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Domain.Services;
using ApiAppShop.Events.Producers;
using Microsoft.Extensions.DependencyInjection;
using ApiAppShop.Cache;

namespace ApiAppShop.CrossCutting.IoC
{
	public class NativeDependencyInjector
	{
		public static void RegisterServices(IServiceCollection services)
		{
			#region Services    
				services.AddScoped<IAppService, AppService>();
				services.AddScoped<IPurchaseService,PurchaseService>();
				services.AddScoped<IUserService, UserService>();			
			#endregion

			#region Repositories    
				services.AddScoped<IAppRepository, AppRepository>();
				services.AddScoped<IUserRepository, UserRepository>();
			#endregion

			#region Events    
				services.AddScoped<IAppPurchasedProducer, AppPurchasedProducer>();
			#endregion

			#region Cache    
				services.AddScoped<ICache, RedisCache>();
			#endregion
		}
	}
}