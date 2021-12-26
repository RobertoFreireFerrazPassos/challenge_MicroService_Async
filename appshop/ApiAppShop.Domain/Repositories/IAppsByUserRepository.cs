using ApiAppShop.Domain.Entities;
using System.Collections.Generic;

namespace ApiAppShop.Domain.Repositories
{
    public interface IAppsByUserRepository
    {
        public void SetAppsByUser(AppsByUserEntity item);
        public void ReplaceUser(AppsByUserEntity item);
        public AppsByUserEntity GetAppsByUserId(string userId);
    }
}
