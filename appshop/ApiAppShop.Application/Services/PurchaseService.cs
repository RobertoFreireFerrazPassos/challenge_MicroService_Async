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
        private readonly IMapper _mapper;

        public PurchaseService(IAppPurchasedProducer appPurchasedProducer,
            IMapper mapper) 
        {
            _appPurchasedProducer = appPurchasedProducer ??
                throw new ArgumentNullException(nameof(appPurchasedProducer));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task PurchaseAsync(AppPurchaseDto AppPurchase) {
            if (AppPurchase.SaveCreditCard) SaveCreditCard();
            await _appPurchasedProducer.Publish(_mapper.Map<AppPurchasedEvent>(AppPurchase));
        }

        private void SaveCreditCard() { 
            // Save Credit Card
        }
    }
}
