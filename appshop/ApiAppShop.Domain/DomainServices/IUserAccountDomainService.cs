using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Domain.DomainServices
{
    public interface IUserAccountDomainService
    {
        public UserAccountEntity Get(string userId);

        public void Update(UserAccountEntity userAccount);

        public void Create(UserAccountEntity userAccount);
    }
}
