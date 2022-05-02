using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Services
{
    public interface IAppService
    {
        public Task AddAppAsync(AppDto addAppRequest);
        public Task<IEnumerable<AppDto>> GetAppsAsync();
        public Task<AppDto> GetAppAsync(string appId);
    }
}