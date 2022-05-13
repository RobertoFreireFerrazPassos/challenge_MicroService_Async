using ApiAppShop.Application.Services;
using ApiAppShop.DataAccess.Repositories;
using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Events.Producers;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Domain.Services;
using ApiAppShop.Events.Producers;
using Microsoft.Extensions.DependencyInjection;
using ApiAppShop.Cache;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Application.DomainServices;

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
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserAccountService, UserAccountService>();
			#endregion

			#region Factories 

			services.AddScoped<IUserAccountEventHandlerFactory, UserAccountEventHandlerFactory>();

			#endregion

			#region DomainServices    

			services.AddScoped<IAppDomainService, AppDomainService>();
			services.AddScoped<IUserAccountDomainService, UserAccountDomainService>();
			services.AddScoped<IUserDomainService, UserDomainService>();

			#endregion

			#region Repositories
			
			services.AddScoped<IAppRepository, AppRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserAccountRepository, UserAccountRepository>();	
			
			#endregion

			#region Events    

			services.AddScoped<IAppPurchasedProducer, AppPurchasedProducer>();

			#endregion

			#region Cache    

			services.AddSingleton<ICache, RedisCache>();

			#endregion
		}
	}
}