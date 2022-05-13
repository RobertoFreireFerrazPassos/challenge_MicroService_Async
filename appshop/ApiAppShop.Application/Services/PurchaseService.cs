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

        private readonly IUserAccountEventHandlerFactory _userEventHandlerFactory;

        public PurchaseService(IAppService appService, 
            IAppPurchasedProducer appPurchasedProducer,
            IUserService userService,
            IUserAccountDomainService userAccountDomainService,
            IMapper mapper,
            IUserAccountEventHandlerFactory userEventHandlerFactory) 
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
            _userEventHandlerFactory = userEventHandlerFactory ??
                throw new ArgumentNullException(nameof(userEventHandlerFactory));
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

            var userEventHandler = _userEventHandlerFactory.Create(
                    new UserAccountEventHandlerFactoryParams() { 
                        UserAccount = userAccount,
                        UserId = appPurchased.UserId,
                        NewPurchaseApp = newPurchaseApp
                    }
                );

            await userEventHandler.Run();
        }
    }

    public interface IUserEventHandler
    {
        public Task Run();
    }

    public class CreateUserEventHandler : IUserEventHandler
    {
        private readonly IUserAccountDomainService _userAccountDomainService;

        private readonly string _userId;

        private readonly AppDto _newPurchaseApp;

        private readonly IMapper _mapper;

        public CreateUserEventHandler(
            IMapper mapper,
            IUserAccountDomainService userAccountDomainService, 
            string userId, 
            AppDto newPurchaseApp)
        {
            _mapper = mapper;

            _userAccountDomainService = userAccountDomainService;

            _userId = userId;

            _newPurchaseApp = newPurchaseApp;
        }

        public async Task Run()
        {
            var userAccount = new UserAccountEntity()
            {
                UserId = _userId,
                Apps = new List<AppEntity>() { _mapper.Map<AppEntity>(_newPurchaseApp) }
            };

            await _userAccountDomainService.CreateAsync(userAccount);
        }
    }

    public class UpdateUserEventHandler : IUserEventHandler
    {
        private readonly IMapper _mapper;

        private readonly IUserAccountDomainService _userAccountDomainService;

        private readonly UserAccountEntity _userAccount;

        private readonly AppDto _newPurchaseApp;

        public UpdateUserEventHandler(
            IMapper mapper,
            IUserAccountDomainService userAccountDomainService,
            UserAccountEntity userAccount, 
            AppDto newPurchaseApp)
        {
            _mapper = mapper;

            _userAccountDomainService = userAccountDomainService;

            _userAccount = userAccount;

            _newPurchaseApp = newPurchaseApp;
        }

        public async Task Run()
        {
            var oldUserAccount = new UserAccountEntity()
            {
                Id = _userAccount.Id,
                UserId = _userAccount.UserId,
                Apps = _userAccount.Apps.ToList()
            };

            var apps = _userAccount.Apps.ToList();

            apps.Add(_mapper.Map<AppEntity>(_newPurchaseApp));

            _userAccount.Apps = apps;

            await _userAccountDomainService.UpdateAsync(_userAccount, oldUserAccount);
        }
    }

    public class NullUserEventHandler : IUserEventHandler
    {
        public async Task Run()
        {
            await Task.CompletedTask;
        }
    }

    public class UserAccountEventHandlerFactoryParams
    {
        public UserAccountEntity UserAccount;

        public AppDto NewPurchaseApp;

        public string UserId;
    }

    public interface IUserAccountEventHandlerFactory
    {
        public IUserEventHandler Create(UserAccountEventHandlerFactoryParams parameters);
    }

    public class UserAccountEventHandlerFactory : IUserAccountEventHandlerFactory
    {
        private readonly IServiceProvider _provider;

        public UserAccountEventHandlerFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IUserEventHandler Create(UserAccountEventHandlerFactoryParams parameters) 
        {
            if (parameters.UserAccount is null)
            {
                return new CreateUserEventHandler(
                        (IMapper) _provider.GetService(typeof(IMapper)),
                        (IUserAccountDomainService) _provider.GetService(typeof(IUserAccountDomainService)),
                        parameters.UserId,
                        parameters.NewPurchaseApp
                    );
            } 
            else if (parameters.UserAccount.Apps.Where(a => a.Id == parameters.NewPurchaseApp.Id).Count() != 0)
            {
                return new NullUserEventHandler();
            }                       

            return new UpdateUserEventHandler(
                    (IMapper) _provider.GetService(typeof(IMapper)),
                    (IUserAccountDomainService) _provider.GetService(typeof(IUserAccountDomainService)),
                    parameters.UserAccount,
                    parameters.NewPurchaseApp
                );
        }
    }
}
