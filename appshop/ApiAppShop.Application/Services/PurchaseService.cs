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
            var app = await _appService.GetAppAsync(appPurchase.AppId);

            appPurchase.AppId = app.Id;

            var user = await _userService.GetUserAsync(appPurchase.UserId);

            await UpdateUserAsync();

            await _appPurchasedProducer.Publish(_mapper.Map<AppPurchasedEvent>(appPurchase));

            async Task UpdateUserAsync()
            {
                if (!appPurchase.SaveCreditCard)
                {
                    return;
                }
                    
                user.CreditCard = appPurchase.CreditCard;

                await _userService.SetUserAsync(user);
            }
        }

        public async Task<IEnumerable<AppDto>> GetAppsByUserAsync(string userId)
        {
            var apps = (await _userAccountDomainService.GetAsync(userId))?.Apps;

            return apps is null ? default(IEnumerable<AppDto>) : _mapper.Map<IEnumerable<AppDto>>(apps);
        }

        public async Task AddAppInUserAccountAsync(AppPurchasedDto appPurchased)
        {
            var newPurchaseApp = await _appService.GetAppAsync(appPurchased.AppId);

            var userAccount = await _userAccountDomainService.GetAsync(appPurchased.UserId);

            if (userAccount is null)
            {
                await CreateNewUserAccountAsync(appPurchased.UserId, newPurchaseApp);
                return;
            }

            if (userAccount.Apps.Where(a => a.Id == newPurchaseApp.Id) is not null)
            {
                return;
            }

            await UpdateUserAccountAsync(userAccount, newPurchaseApp);

            async Task CreateNewUserAccountAsync(string userId, AppDto newPurchaseApp)
            {
                userAccount = new UserAccountEntity()
                {
                    UserId = userId,
                    Apps = new List<AppEntity>() { _mapper.Map<AppEntity>(newPurchaseApp) }
                };

                await _userAccountDomainService.CreateAsync(userAccount);
            }

            async Task UpdateUserAccountAsync(UserAccountEntity userAccount, AppDto newPurchaseApp)
            {
                var apps = userAccount.Apps.ToList();

                apps.Add(_mapper.Map<AppEntity>(newPurchaseApp));

                userAccount.Apps = apps;

                await _userAccountDomainService.UpdateAsync(userAccount);
            }
        }
    }
}
