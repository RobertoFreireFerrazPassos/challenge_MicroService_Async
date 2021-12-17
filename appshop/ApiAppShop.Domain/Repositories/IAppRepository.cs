using ApiAppShop.Domain.Entities;
using System.Collections.Generic;

namespace ApiAppShop.Domain.Repositories
{
    public interface IAppRepository
    {
        public AppEntity GetApp(string id);
        public IEnumerable<AppEntity> GetApps();
        public void SetApp(AppEntity item);
    }
}
