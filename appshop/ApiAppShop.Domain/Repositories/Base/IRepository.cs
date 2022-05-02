using ApiAppShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Domain.Repositories.Base
{
	public interface IRepository<T> where T : Entity
	{
        public Task SetItemAsync(T item);
        public Task<IEnumerable<T>> GetItemsAsync();
        public Task<T> GetItemAsync(string id);
        public Task UpdateItemAsync(string itemId, string field, object value);
        public Task ReplaceItemAsync(T item);
    }
}