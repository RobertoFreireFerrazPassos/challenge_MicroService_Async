using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiUser.Domain.Entities;
using ApiUser.DataAccess.Repositories.Base;
using ApiUser.Domain.Repositories;

namespace ApiUser.DataAccess.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) {}
                    
        public async Task<bool> AddUserAsync(UserEntity user)
        {
            string query = $@"";

            await AddAsync(user, query);
            return true;
        }
    }
}

