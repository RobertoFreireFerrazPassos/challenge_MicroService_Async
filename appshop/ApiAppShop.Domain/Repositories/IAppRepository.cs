using ApiAppShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Repositories
{
    public interface IAppRepository
    {
        public Task<AppEntity> GetAppAsync(string id);
        public Task<IEnumerable<AppEntity>> GetAppsAsync();
        public Task SetAppAsync(AppEntity item);
    }
}
