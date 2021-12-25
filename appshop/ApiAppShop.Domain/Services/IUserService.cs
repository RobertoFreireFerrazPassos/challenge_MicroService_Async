using ApiAppShop.Domain.Dtos.User;

namespace ApiAppShop.Domain.Services
{
    public interface IUserService
    {
        public UserDto GetUser(string userId);
        public void SetUser(UserDto user);
    }
}
