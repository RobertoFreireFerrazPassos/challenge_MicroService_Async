using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Domain.DomainServices
{
    public interface IUserDomainService
    {
        public UserEntity GetUserById(string userId);
        public UserEntity GetUserByName(string name);
        public void CreateNewUser(UserEntity user);
        public void UpdateUser(UserEntity user);        
    }
}
