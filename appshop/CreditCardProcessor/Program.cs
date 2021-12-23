namespace ApiAppShop
{
    using CreditCardProcessor.Events.Infrastructure;
    using MassTransit;
    using MassTransit.RabbitMqTransport;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {                  
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("amqp://guest:guest@rabbitmq:5672");
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
