using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ApiAppShop.DataAccess.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private static readonly string Document = NoSqlDocumentConstants.USERS;
        public UserRepository(IConfiguration configuration) : base(configuration, Document)
        {
        }

        public UserEntity GetUser(string id)
        {
            return GetItem(id);
        }

        public void SetUser(UserEntity item)
        {
            SetItem(item);
        }
    }
}
