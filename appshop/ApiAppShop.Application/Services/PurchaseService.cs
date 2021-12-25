using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Events;
using ApiAppShop.Domain.Events.Producers;
using ApiAppShop.Domain.Repositories;
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
        private readonly IUserRepository _userRepository;
        private IAppService _appService;
        private readonly IMapper _mapper;

        public PurchaseService(IAppService appService, 
            IAppPurchasedProducer appPurchasedProducer,
            IUserRepository userRepository,
            IMapper mapper) 
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _appService = appService ??
                throw new ArgumentNullException(nameof(appService));
            _appPurchasedProducer = appPurchasedProducer ??
                throw new ArgumentNullException(nameof(appPurchasedProducer));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task PurchaseAsync(AppPurchaseDto appPurchase) {
            string userId = appPurchase.UserId;
            var user = ValidateUser(userId);
            ValidateApp(appPurchase.AppId);            

            if (appPurchase.SaveCreditCard) SaveCreditCard(user, appPurchase.CreditCard);
            await _appPurchasedProducer.Publish(_mapper.Map<AppPurchasedEvent>(appPurchase));
        }

        private void SaveCreditCard(UserEntity user, CreditCard creditCard) {
            user.CreditCard = creditCard;
           _userRepository.SetUser(user);
        }

        private UserEntity ValidateUser(string userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null) throw new Exception(ErrorMessageConstants.USER_DOESNT_EXIST);
            return user;
        }

        private void ValidateApp(string appId)
        {
            var app = _appService.GetApp(appId);

            if (app == null) throw new Exception(ErrorMessageConstants.APP_DOESNT_EXIST);
        }
    }
}
