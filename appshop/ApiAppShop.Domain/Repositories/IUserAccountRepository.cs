using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Domain.Repositories
{
    public interface IUserAccountRepository
    {
        public void Set(UserAccountEntity item);
        public void Replace(UserAccountEntity item);
        public UserAccountEntity Get(string userId);
    }
}
