using System.Collections.Generic;

namespace ApiAppShop.Domain.Dtos
{
    public class UserAccountDto
    {
        public string UserId { get; set; }
        public IEnumerable<AppDto> Apps { get; set; }
    }
}
