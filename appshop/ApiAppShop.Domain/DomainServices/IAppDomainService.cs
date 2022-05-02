using ApiAppShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.DomainServices
{
    public interface IAppDomainService
    {
        public Task AddAppAsync(AppEntity app);

        public Task<IEnumerable<AppEntity>> GetAppsAsync();

        public Task<AppEntity> GetAppAsync(string appId);
    }
}
