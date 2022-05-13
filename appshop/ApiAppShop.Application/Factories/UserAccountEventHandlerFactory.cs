using ApiAppShop.Application.Handlers;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Factories;
using ApiAppShop.Domain.Handlers;
using ApiAppShop.Domain.Params;
using AutoMapper;
using System;
using System.Linq;

namespace ApiAppShop.Application.Factories
{
    public class UserAccountEventHandlerFactory : IUserAccountEventHandlerFactory
    {
        private readonly IServiceProvider _provider;

        public UserAccountEventHandlerFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IUserAccountEventHandler Create(UserAccountEventHandlerFactoryParams parameters)
        {
            if (parameters.UserAccount is null)
            {
                return new CreateUserAccountEventHandler(
                        (IMapper)_provider.GetService(typeof(IMapper)),
                        (IUserAccountDomainService)_provider.GetService(typeof(IUserAccountDomainService)),
                        parameters.UserId,
                        parameters.NewPurchaseApp
                    );
            }
            else if (parameters.UserAccount.Apps.Where(a => a.Id == parameters.NewPurchaseApp.Id).Count() != 0)
            {
                return new NullUserAccountEventHandler();
            }

            return new UpdateUserAccountEventHandler(
                    (IMapper)_provider.GetService(typeof(IMapper)),
                    (IUserAccountDomainService)_provider.GetService(typeof(IUserAccountDomainService)),
                    parameters.UserAccount,
                    parameters.NewPurchaseApp
                );
        }
    }
}
