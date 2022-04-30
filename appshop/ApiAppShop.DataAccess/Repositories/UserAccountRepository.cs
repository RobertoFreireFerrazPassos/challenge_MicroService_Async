using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Repository;
using Microsoft.Extensions.Configuration;

namespace ApiAppShop.DataAccess.Repositories
{
    public class UserAccountRepository : Repository<UserAccountEntity>, IUserAccountRepository
    {
        private static readonly string Document = NoSqlDocumentConstants.APPSBYUSER;
        public UserAccountRepository(IConfiguration configuration) : base(configuration, Document)
        {
        }

        public void Set(UserAccountEntity item)
        {
            SetItem(item);
        }

        public void Replace(UserAccountEntity item)
        {
            ReplaceItem(item);
        }

        public UserAccountEntity Get(string userId)
        {
            return GetItemByCustomStringFilter(RepositoryConstants.USERID, userId);
        }
    }
}
