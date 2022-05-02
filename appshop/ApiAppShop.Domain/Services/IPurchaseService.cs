using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Services
{
    public interface IPurchaseService
    {
        public Task PurchaseAsync(AppPurchaseDto AppPurchase);

        public Task AddAppInUserAccountAsync(AppPurchasedDto appPurchased);

        public Task<IEnumerable<AppDto>> GetAppsByUserAsync(string userId);
    }
}
