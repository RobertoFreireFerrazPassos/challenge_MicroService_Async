using ApiAppShop.Domain.ValueObjects;

namespace ApiAppShop.Domain.Dtos
{
    public class AppPurchaseDto
    {
        public string AppId { get; set; }
        public string UserId { get; set; }
        public bool SaveCreditCard { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
