using ApiAppShop.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CreditCardProcessor.Events.Producers
{
    public class AppPurchasedStatusConfirmationProducer
    {
        public static IConfigurationRoot configuration;
        public static async Task Publish(AppPurchasedStatusConfirmationEvent appPurchasedStatusConfirmation)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
            });

            Console.WriteLine("Publishing AppPurchasedStatusConfirmation...");

            await busControl.Publish<AppPurchasedStatusConfirmationEvent>(appPurchasedStatusConfirmation);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        }
    }
}
