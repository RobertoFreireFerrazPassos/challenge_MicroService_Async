using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Events;
using ApiAppShop.Domain.Events.Producers;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppShop.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IAppPurchasedProducer _appPurchasedProducer;
        
        private readonly IUserService _userService;
        
        private readonly IAppService _appService;

        private readonly IUserAccountDomainService _userAccountDomainService;

        private readonly IMapper _mapper;

        public PurchaseService(IAppService appService, 
            IAppPurchasedProducer appPurchasedProducer,
            IUserService userService,
            IUserAccountDomainService userAccountDomainService,
            IMapper mapper) 
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _appService = appService ??
                throw new ArgumentNullException(nameof(appService));
            _appPurchasedProducer = appPurchasedProducer ??
                throw new ArgumentNullException(nameof(appPurchasedProducer));
            _userAccountDomainService = userAccountDomainService ??
                throw new ArgumentNullException(nameof(userAccountDomainService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task PurchaseAsync(AppPurchaseDto appPurchase) {
            var app = _appService.GetApp(appPurchase.AppId);

            appPurchase.AppId = app.Id;

            var user = _userService.GetUser(appPurchase.UserId);

            UpdateUser();

            await _appPurchasedProducer.Publish(_mapper.Map<AppPurchasedEvent>(appPurchase));

            void UpdateUser()
            {
                if (!appPurchase.SaveCreditCard)
                {
                    return;
                }
                    
                user.CreditCard = appPurchase.CreditCard;

                _userService.SetUser(user);
            }
        }

        public IEnumerable<AppDto> GetAppsByUser(string userId)
        {
            return _mapper.Map<IEnumerable<AppDto>>(_userAccountDomainService.Get(userId).Apps);
        }

        public void AddAppInUserAccount(AppPurchasedDto appPurchased)
        {
            var newPurchaseApp = _appService.GetApp(appPurchased.AppId);

            var userAccount = _userAccountDomainService.Get(appPurchased.UserId);

            if (userAccount is null)
            {
                CreateNewUserAccount(appPurchased.UserId, newPurchaseApp);
                return;
            }

            if (userAccount.Apps.Where(a => a.Id == newPurchaseApp.Id) is not null)
            {
                return;
            }

            UpdateUserAccount(userAccount, newPurchaseApp);

            void CreateNewUserAccount(string userId, AppDto newPurchaseApp)
            {
                userAccount = new UserAccountEntity()
                {
                    UserId = userId,
                    Apps = new List<AppEntity>() { _mapper.Map<AppEntity>(newPurchaseApp) }
                };

                _userAccountDomainService.Create(userAccount);
            }

            void UpdateUserAccount(UserAccountEntity userAccount, AppDto newPurchaseApp)
            {
                var apps = userAccount.Apps.ToList();

                apps.Add(_mapper.Map<AppEntity>(newPurchaseApp));

                userAccount.Apps = apps;

                _userAccountDomainService.Update(userAccount);
            }
        }
    }
}
