using ApiAppShop.Domain.Entities;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.DomainServices
{
    public interface IUserDomainService
    {
        public Task<UserEntity> GetUserByIdAsync(string userId);
        public Task<UserEntity> GetUserByNameAsync(string name);
        public Task CreateNewUserAsync(UserEntity user);
        public Task UpdateUserAsync(UserEntity user);        
    }
}
