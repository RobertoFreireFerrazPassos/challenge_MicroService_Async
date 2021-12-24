using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Events;
using ApiAppShop.Domain.Services;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Consumers
{
    public class AppPurchasedStatusConfirmationConsumer : IConsumer<AppPurchasedStatusConfirmationEvent>
    {
        private readonly IAppService _appService;

        public AppPurchasedStatusConfirmationConsumer(IAppService appService) 
        {
            _appService = appService;
        }

        public async Task Consume(ConsumeContext<AppPurchasedStatusConfirmationEvent> context)
        {
            string message;
            if (context.Message.StatusConfirmation)
            {
                message = $"Purchase made by user {context.Message.UserId} on app {context.Message.AppId}";
                var newApp = new AppCreationDto()
                {
                    UserId = context.Message.UserId,
                    Name = "Teste 2",
                    Price = 4
                };

                _appService.AddAppByUser(newApp);
            }
            else
            {
                message = $"Purchase denied";
            }                
            
            Console.WriteLine(message);
        }
    }
}
