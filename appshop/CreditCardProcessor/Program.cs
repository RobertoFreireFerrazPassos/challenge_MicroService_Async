namespace CreditCardProcessor
{
    using CreditCardProcessor.Events.Consumers;
    using MassTransit;
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
                cfg.ReceiveEndpoint("app-purchased", e =>
                {
                    e.Consumer<AppPurchasedConsumer>();
                });
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
    }
}
