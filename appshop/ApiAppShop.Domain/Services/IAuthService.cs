using ApiAppShop.Domain.Dtos.User;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Services
{
    public interface IAuthService
    {
        public Task<string> LogInAsync(LogInDto logInInfo);

        public Task CreateNewUserAsync(UserDto user);
    }
}
