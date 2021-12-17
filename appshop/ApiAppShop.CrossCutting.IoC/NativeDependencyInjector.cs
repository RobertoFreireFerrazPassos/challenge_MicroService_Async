using ApiAppShop.DataAccess.Repositories;
using ApiAppShop.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiAppShop.CrossCutting.IoC
{
	public class NativeDependencyInjector
	{
		public static void RegisterServices(IServiceCollection services)
		{
            services.AddScoped<IAppRepository, AppRepository>();
		}
    }
}