using ApiAppShop.Domain.Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace CreditCardProcessor.Events.Infrastructure
{
    public class ReceiveEndpoints
    {
        public static void RegisterReceiveEndpoints(IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ReceiveEndpoint("app-purchased", e =>
            {
                e.Consumer<AppPurchasedConsumer>();
            });
        }
    }
}
