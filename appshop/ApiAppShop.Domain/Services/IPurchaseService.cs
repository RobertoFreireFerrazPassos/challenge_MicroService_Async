using ApiAppShop.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Services
{
    public interface IPurchaseService
    {
        public Task PurchaseAsync(AppPurchaseDto AppPurchase);

        public void AddAppInUserAccount(AppPurchasedDto appPurchased);

        public IEnumerable<AppDto> GetAppsByUser(string userId);
    }
}
