using ApiAppShop.Application.DataContracts.Common;

namespace ApiUser.Application.DataContracts
{
    public class PurchaseRequest
    {
        public string AppId { get; set; }
        public string UserId { get; set; }
        public bool SaveCreditCard { get; set; }
        public CreditCard CreditCard { get; set; }        
    }
}
