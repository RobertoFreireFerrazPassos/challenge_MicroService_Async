using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Factories;
using ApiAppShop.Domain.Params;
using ApiAppShop.Domain.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Application.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountDomainService _userAccountDomainService;

        private readonly IMapper _mapper;

        private readonly IUserAccountEventHandlerFactory _userAccountEventHandlerFactory;

        private readonly IAppService _appService;

        public UserAccountService(
            IAppService appService,
            IUserAccountDomainService userAccountDomainService,
            IMapper mapper,
            IUserAccountEventHandlerFactory userAccountEventHandlerFactory)
        {
            _appService = appService ??
                throw new ArgumentNullException(nameof(appService));            
            _userAccountDomainService = userAccountDomainService ??
                throw new ArgumentNullException(nameof(userAccountDomainService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userAccountEventHandlerFactory = userAccountEventHandlerFactory ??
                throw new ArgumentNullException(nameof(userAccountEventHandlerFactory));
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

            var userAccountEventHandler = _userAccountEventHandlerFactory.Create(
                    new UserAccountEventHandlerFactoryParams()
                    {
                        UserAccount = userAccount,
                        UserId = appPurchased.UserId,
                        NewPurchaseApp = newPurchaseApp
                    }
                );

            await userAccountEventHandler.Run();
        }
    }
}
