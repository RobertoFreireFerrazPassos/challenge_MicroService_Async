using ApiAppShop.Domain.Entities;
using System.Collections.Generic;

namespace ApiAppShop.Domain.Repositories.Base
{
	public interface IRepository<T> where T : Entity
	{
        public void SetItem(T item);
        public IEnumerable<T> GetItems();
        public T GetItem(string id);
    }
}