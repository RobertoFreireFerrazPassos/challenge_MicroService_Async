using ApiAppShop.Domain.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace ApiAppShop.CrossCutting.IoC
{
    public class RegisterMassTransitService
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            services.AddMassTransit(bus =>
            {
                bus.AddConsumer<AppPurchasedStatusConfirmationConsumer>();

                bus.UsingRabbitMq((context, busConfigurator) =>
                {
                    busConfigurator.Host(connectionString);

                    busConfigurator.ReceiveEndpoint("AppPurchased_StatusConfirmation", e =>
                    {
                        e.ConfigureConsumer<AppPurchasedStatusConfirmationConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();
        }
    }
}
