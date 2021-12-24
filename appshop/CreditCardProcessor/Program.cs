namespace ApiAppShop
{
    using CreditCardProcessor.Events.Infrastructure;
    using MassTransit;
    using MassTransit.RabbitMqTransport;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        public static IConfigurationRoot configuration;

        public static async Task Main()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
                RegisterReceiveEndpoints(cfg);                
            });

            Console.WriteLine("Waiting for messages...");

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        }

        private static void RegisterReceiveEndpoints(IRabbitMqBusFactoryConfigurator configurator)
        {
            ReceiveEndpoints.RegisterReceiveEndpoints(configurator);
        }
    }
}
