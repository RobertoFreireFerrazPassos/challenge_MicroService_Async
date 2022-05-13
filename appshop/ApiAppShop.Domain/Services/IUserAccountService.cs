using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Services
{
    public interface IUserAccountService
    {
        public Task AddAppInUserAccountAsync(AppPurchasedDto appPurchased);

        public Task<IEnumerable<AppDto>> GetAppsByUserAsync(string userId);
    }
}
