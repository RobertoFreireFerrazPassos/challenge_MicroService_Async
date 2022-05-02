using ApiAppShop.Domain.Entities;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserAsync(string id);
        public Task<UserEntity> GetUserByNameAsync(string name);
        public Task SetUserAsync(UserEntity item);
        public Task UpdateUserAsync(string userId, string field, object value);
        public Task ReplaceUserAsync(UserEntity item);
    }
}
