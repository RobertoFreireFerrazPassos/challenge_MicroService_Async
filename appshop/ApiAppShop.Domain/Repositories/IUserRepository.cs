using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Domain.Repositories
{
    public interface IUserRepository
    {
        public UserEntity GetUser(string id);

        public void SetUser(UserEntity item);
    }
}
