using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Repository;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ApiAppShop.DataAccess.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private static readonly string Document = NoSqlDocumentConstants.USERS;

        public UserRepository(IConfiguration configuration) : base(configuration, Document) { }

        public async Task<UserEntity> GetUserAsync(string id)
        {
            return await GetItemAsync(id);
        }

        public async Task<UserEntity> GetUserByNameAsync(string name)
        {
            return await GetItemByCustomStringFilterAsync("Name", name);
        }        

        public async Task SetUserAsync(UserEntity item)
        {
            await SetItemAsync(item);
        }

        public async Task UpdateUserAsync(string userId, string field, object value)
        {
            await UpdateItemAsync(userId, field, value);
        }

        public async Task ReplaceUserAsync(UserEntity item)
        {
            await ReplaceItemAsync(item);
        }
    }
}
