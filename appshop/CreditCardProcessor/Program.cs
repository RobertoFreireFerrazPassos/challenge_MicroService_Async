using CreditCardProcessor.Events.Consumers;
using MassTransit;
using System;

namespace CreditCardProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("amqp://guest:guest@rabbitmq:5672");
                cfg.ReceiveEndpoint("app-purchased", e =>
                {
                    e.Consumer<AppPurchasedConsumer>();
                    e.PrefetchCount = 10;
                });
            });
            busControl.Start();

            Console.WriteLine("Waiting for messages...");

            while (true) ;
        }
    }
}
