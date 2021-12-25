using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Events;
using ApiAppShop.Domain.Events.Producers;
using ApiAppShop.Domain.Services;
using ApiAppShop.Domain.ValueObjects;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace ApiAppShop.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IAppPurchasedProducer _appPurchasedProducer;
        private readonly IUserService _userService;
        private IAppService _appService;
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
            string userId = appPurchase.UserId;
            string appId = appPurchase.AppId;
            var user = _userService.GetUser(userId);
            _appService.ValidateApp(appId);
            if (appPurchase.SaveCreditCard) SaveCreditCard(user, appPurchase.CreditCard);
            await _appPurchasedProducer.Publish(_mapper.Map<AppPurchasedEvent>(appPurchase));
        }

        private void SaveCreditCard(UserDto user, CreditCard creditCard) {
            user.CreditCard = creditCard;
            _userService.SetUser(user);
        }
    }
}
