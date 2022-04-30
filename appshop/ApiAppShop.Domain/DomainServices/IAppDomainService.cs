using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using System.Collections.Generic;

namespace ApiAppShop.Domain.DomainServices
{
    public interface IAppDomainService
    {
        public void AddApp(AppEntity app);

        public IEnumerable<AppEntity> GetApps();

        public AppEntity GetApp(string appId);
    }
}
