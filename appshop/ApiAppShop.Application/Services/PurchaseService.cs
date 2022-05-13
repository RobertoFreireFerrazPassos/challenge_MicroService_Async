using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Events;
using ApiAppShop.Domain.Events.Producers;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace ApiAppShop.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IAppPurchasedProducer _appPurchasedProducer;
        
        private readonly IUserService _userService;
        
        private readonly IAppService _appService;

        private readonly IMapper _mapper;

        public PurchaseService(IAppService appService, 
            IAppPurchasedProducer appPurchasedProducer,
            IUserService userService,
            IMapper mapper) 
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _appService = appService ??
                throw new ArgumentNullException(nameof(appService));
            _appPurchasedProducer = appPurchasedProducer ??
                throw new ArgumentNullException(nameof(appPurchasedProducer));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task PurchaseAsync(AppPurchaseDto appPurchase) {
            var app = await _appService.GetAppAsync(appPurchase.AppId);

            appPurchase.AppId = app.Id;

            var user = await _userService.GetUserAsync(appPurchase.UserId);
            
            if (appPurchase.SaveCreditCard)
            {
                user.CreditCard = appPurchase.CreditCard;

                await _userService.SetUserAsync(user);
            }

            await _appPurchasedProducer.Publish(_mapper.Map<AppPurchasedEvent>(appPurchase));
        }
    }
}
