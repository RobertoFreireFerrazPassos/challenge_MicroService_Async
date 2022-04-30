using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Domain.DomainServices
{
    public interface IUserDomainService
    {
        public UserEntity GetUser(string userId);

        public void SetUser(UserDto user);
    }
}
