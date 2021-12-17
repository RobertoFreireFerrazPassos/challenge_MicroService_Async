using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerPortalPersistence.CrossCutting.IoC
{
	public class NativeDependencyInjector
	{
		public static void RegisterServices(IServiceCollection services,
			IConfiguration configuration)
		{
			// Infrastructure
			services.AddSingleton<Interface, Class>();

            // Services
            services.AddScoped<Interface, Class>();

			services.AddTransient<Context>();
		}
    }
}