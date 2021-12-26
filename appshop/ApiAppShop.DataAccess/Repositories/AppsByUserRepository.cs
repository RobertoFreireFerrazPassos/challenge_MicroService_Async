using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Repository;
using Microsoft.Extensions.Configuration;

namespace ApiAppShop.DataAccess.Repositories
{
    public class AppsByUserRepository : Repository<AppsByUserEntity>, IAppsByUserRepository
    {
        private static readonly string Document = NoSqlDocumentConstants.APPSBYUSER;
        public AppsByUserRepository(IConfiguration configuration) : base(configuration, Document)
        {
        }

        public void SetAppsByUser(AppsByUserEntity item)
        {
            SetItem(item);
        }

        public void ReplaceUser(AppsByUserEntity item)
        {
            ReplaceItem(item);
        }

        public AppsByUserEntity GetAppsByUserId(string userId)
        {
            return GetItemByCustomStringFilter(RepositoryConstants.USERID, userId);
        }
    }
}
