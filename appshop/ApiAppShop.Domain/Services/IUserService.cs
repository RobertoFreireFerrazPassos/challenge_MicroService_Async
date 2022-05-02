using ApiAppShop.Domain.Dtos.User;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Services
{
    public interface IUserService
    {
        public Task<UserDto> GetUserAsync(string userId);
        public Task SetUserAsync(UserDto user);
    }
}
