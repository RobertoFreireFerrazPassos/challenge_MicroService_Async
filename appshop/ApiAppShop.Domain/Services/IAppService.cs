using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;

namespace ApiAppShop.Domain.Services
{
    public interface IAppService
    {
        public void AddApp(AppDto addAppRequest);
        public IEnumerable<AppDto> GetApps();
        public AppDto GetApp(string appId);
    }
}