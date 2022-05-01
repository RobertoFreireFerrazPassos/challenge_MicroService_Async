using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Domain.Repositories
{
    public interface IUserRepository
    {
        public UserEntity GetUser(string id);
        public UserEntity GetUserByName(string name);
        public void SetUser(UserEntity item);
        public void UpdateUser(string userId, string field, object value);
        public void ReplaceUser(UserEntity item);
    }
}
