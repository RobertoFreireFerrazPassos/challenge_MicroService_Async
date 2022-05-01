using ApiAppShop.Domain.Dtos.User;

namespace ApiAppShop.Domain.Services
{
    public interface IAuthService
    {
        public string LogIn(LogInDto logInInfo);

        public void CreateNewUser(UserDto user);
    }
}
