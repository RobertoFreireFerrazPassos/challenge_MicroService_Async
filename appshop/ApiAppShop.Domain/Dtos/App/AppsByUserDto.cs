using System.Collections.Generic;

namespace ApiAppShop.Domain.Dtos
{
    public class AppsByUserDto
    {
        public string UserId { get; set; }
        public IEnumerable<AppDto> Apps { get; set; }
    }
}
