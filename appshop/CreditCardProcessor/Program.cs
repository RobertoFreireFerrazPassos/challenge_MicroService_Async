namespace ApiAppShop
{
    using CreditCardProcessor.Events.Infrastructure;
    using CreditCardProcessor.Infrastructure;
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
        public static async Task Main()
        {
            IConfigurationRoot configuration = Appsettings.GetConfiguration();

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

        private static void RegisterReceiveEndpoints(IRabbitMqBusFactoryConfigurator configurator)
        {
            ReceiveEndpoints.RegisterReceiveEndpoints(configurator);
        }
    }
}
