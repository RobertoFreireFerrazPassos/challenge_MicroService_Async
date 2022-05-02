using ApiAppShop.Domain.Constants;
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
        private readonly IPurchaseService _purchaseService;

        public AppPurchasedStatusConfirmationConsumer(IPurchaseService purchaseService) 
        {
            _purchaseService = purchaseService;
        }

        public async Task Consume(ConsumeContext<AppPurchasedStatusConfirmationEvent> context)
        {
            string message;

            if (context.Message.StatusConfirmation)
            {
                message = String.Format(LogMessageConstants.PURCHASE_MADE_BY_USER_0_ON_APP_1,context.Message.UserId,context.Message.AppId);
                var newApp = new AppPurchasedDto()
                {
                    UserId = context.Message.UserId,
                    AppId = context.Message.AppId
                };

                await _purchaseService.AddAppInUserAccountAsync(newApp);
            }
            else
            {
                message = LogMessageConstants.PURCHASE_DENIED;
            }                
            
            Console.WriteLine(message);
        }
    }
}
