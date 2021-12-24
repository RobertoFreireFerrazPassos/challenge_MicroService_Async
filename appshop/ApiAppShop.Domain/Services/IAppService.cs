using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;

namespace ApiAppShop.Domain.Services
{
    public interface IAppService
    {
        public void AddAppByUser(AppCreationDto appCreationRequest);
        public IEnumerable<AppDto> GetAppsByUser(string userId);
    }
}
