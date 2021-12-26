using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;

namespace ApiAppShop.Domain.Services
{
    public interface IAppService
    {
        public void AddAppByUser(AppPurchasedDto appCreationRequest);
        public void AddApp(AppDto addAppRequest);
        public IEnumerable<AppDto> GetApps();
        public AppDto GetApp(string appId);
        public bool ValidateApp(string appId);
        public List<AppDto> GetAppsByUser(string userId, out bool areAppsbyUserFromDatabase);
    }
}