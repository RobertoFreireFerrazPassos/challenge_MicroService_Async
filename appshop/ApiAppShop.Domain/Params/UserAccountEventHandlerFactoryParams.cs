using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Domain.Params
{
    public class UserAccountEventHandlerFactoryParams
    {
        public UserAccountEntity UserAccount;

        public AppDto NewPurchaseApp;

        public string UserId;
    }
}
