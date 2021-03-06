using ApiAppShop.Domain.Entities;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.DomainServices
{
    public interface IUserAccountDomainService
    {
        public Task<UserAccountEntity> GetAsync(string userId);

        public Task UpdateAsync(UserAccountEntity userAccount, UserAccountEntity oldUserAccount);

        public Task CreateAsync(UserAccountEntity userAccount);
    }
}
