using ApiAppShop.Domain.Dtos;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Services
{
    public interface IPurchaseService
    {
        public Task PurchaseAsync(AppPurchaseDto AppPurchase);
    }
}
