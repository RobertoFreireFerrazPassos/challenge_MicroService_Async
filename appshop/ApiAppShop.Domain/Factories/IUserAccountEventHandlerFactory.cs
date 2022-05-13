using ApiAppShop.Domain.Handlers;
using ApiAppShop.Domain.Params;

namespace ApiAppShop.Domain.Factories
{

    public interface IUserAccountEventHandlerFactory
    {
        public IUserAccountEventHandler Create(UserAccountEventHandlerFactoryParams parameters);
    }
}
