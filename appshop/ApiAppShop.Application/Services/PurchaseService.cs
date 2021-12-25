using ApiAppShop.Domain.Constants;
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
        private IAppService _appService;
        private readonly IMapper _mapper;

        public PurchaseService(IAppService appService, 
            IAppPurchasedProducer appPurchasedProducer,
            IMapper mapper) 
        {
            _appService = appService ??
                throw new ArgumentNullException(nameof(appService));
            _appPurchasedProducer = appPurchasedProducer ??
                throw new ArgumentNullException(nameof(appPurchasedProducer));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task PurchaseAsync(AppPurchaseDto appPurchase) {
            if (appPurchase.SaveCreditCard) SaveCreditCard();
            ValidateApp(appPurchase.AppId);
            await _appPurchasedProducer.Publish(_mapper.Map<AppPurchasedEvent>(appPurchase));
        }

        private void SaveCreditCard() { 
            // Save Credit Card
        }

        private void ValidateApp(string appId)
        {
            var app = _appService.GetApp(appId);

            if (app == null) throw new Exception(ErrorMessageConstants.APP_DOESNT_EXIST);
        }
    }
}
