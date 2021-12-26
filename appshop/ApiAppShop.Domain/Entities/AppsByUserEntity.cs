using System.Collections.Generic;

namespace ApiAppShop.Domain.Entities
{
    public class AppsByUserEntity : Entity
    {
        public string UserId { get; set; }
        public IEnumerable<AppEntity> Apps { get; set; }
    }
}
