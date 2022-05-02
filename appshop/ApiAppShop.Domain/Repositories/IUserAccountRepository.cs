using ApiAppShop.Domain.Entities;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Repositories
{
    public interface IUserAccountRepository
    {
        public Task SetAsync(UserAccountEntity item);
        public Task ReplaceAsync(UserAccountEntity item);
        public Task<UserAccountEntity> GetAsync(string userId);
    }
}
