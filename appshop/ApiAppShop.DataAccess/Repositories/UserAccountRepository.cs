using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Repository;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ApiAppShop.DataAccess.Repositories
{
    public class UserAccountRepository : Repository<UserAccountEntity>, IUserAccountRepository
    {
        private static readonly string Document = NoSqlDocumentConstants.APPSBYUSER;
        public UserAccountRepository(IConfiguration configuration) : base(configuration, Document)
        {
        }

        public async Task SetAsync(UserAccountEntity item)
        {
            await SetItemAsync(item);
        }

        public async Task ReplaceAsync(UserAccountEntity item)
        {
            await ReplaceItemAsync(item);
        }

        public async Task<UserAccountEntity> GetAsync(string userId)
        {
            return await GetItemByCustomStringFilterAsync(RepositoryConstants.USERID, userId);
        }
    }
}
