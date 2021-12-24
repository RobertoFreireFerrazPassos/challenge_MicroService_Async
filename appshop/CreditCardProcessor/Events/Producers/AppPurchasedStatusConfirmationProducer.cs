using ApiAppShop.Domain.Events;
using CreditCardProcessor.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CreditCardProcessor.Events.Producers
{
    public class AppPurchasedStatusConfirmationProducer
    {
        public static async Task Publish(AppPurchasedStatusConfirmationEvent appPurchasedStatusConfirmation)
        {
            IConfigurationRoot configuration = Appsettings.GetConfiguration();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
            });

            Console.WriteLine("Publishing AppPurchasedStatusConfirmation...");

            await busControl.Publish<AppPurchasedStatusConfirmationEvent>(appPurchasedStatusConfirmation);
        }
    }
}
