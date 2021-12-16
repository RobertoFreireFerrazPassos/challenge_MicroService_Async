using System.Threading.Tasks;
using ApiUser.Domain.Entities;

namespace ApiUser.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(UserEntity user);
    }
}