using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.DataAccess.Repositories
{
    public class AppRepository : Repository<AppEntity>, IAppRepository
    {
        private static readonly string Document = NoSqlDocumentConstants.APPS;
        public AppRepository(IConfiguration configuration) : base(configuration, Document)
        {
        }

        public async Task<AppEntity> GetAppAsync(string id)
        {
            return await GetItemAsync(id);
        }

        public async Task<IEnumerable<AppEntity>> GetAppsAsync()
        {
            return await GetItemsAsync();
        }

        public async Task SetAppAsync(AppEntity item)
        {
            await SetItemAsync(item);
        }
    }
}
