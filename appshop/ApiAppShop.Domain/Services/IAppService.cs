using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;

namespace ApiAppShop.Domain.Services
{
    public interface IAppService
    {
        public void SetItem(AppCreationDto appCreationRequest);
        public IEnumerable<AppDto> GetItems();
        public IEnumerable<AppDto> GetItem(string key);
    }
}
