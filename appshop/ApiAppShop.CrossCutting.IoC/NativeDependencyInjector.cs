using ApiAppShop.Application.Services;
using ApiAppShop.DataAccess.Repositories;
using ApiAppShop.Domain.Events.Producers;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Domain.Services;
using ApiAppShop.Events.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace ApiAppShop.CrossCutting.IoC
{
	public class NativeDependencyInjector
	{
		public static void RegisterServices(IServiceCollection services)
		{
			#region Services    
				services.AddScoped<IAppService, AppService>();
				services.AddScoped<IPurchaseService,PurchaseService>();			
			#endregion

			#region Repositories    
				services.AddScoped<IAppRepository, AppRepository>();
			#endregion

			#region Events    
				services.AddScoped<IAppPurchasedProducer, AppPurchasedProducer>();
			#endregion
		}
	}
}