using ApiAppShop.Domain.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace CreditCardProcessor.Events.Producers
{
    public class AppPurchasedStatusConfirmationProducer
    {
        public static async Task Publish(AppPurchasedStatusConfirmationEvent appPurchasedStatusConfirmation)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("amqp://guest:guest@rabbitmq:5672");
            });

            Console.WriteLine("Publishing AppPurchasedStatusConfirmation...");

            await busControl.Publish<AppPurchasedStatusConfirmationEvent>(appPurchasedStatusConfirmation);
        }
    }
}
