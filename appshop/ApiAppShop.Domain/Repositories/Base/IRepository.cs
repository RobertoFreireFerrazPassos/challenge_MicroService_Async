using CustomerPortalPersistence.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CustomerPortalPersistence.Domain.Repositories.Base
{
	public interface IRepository<T> where T : Entity
	{
		Task AddAsync(T entity, string query);
    }
}