using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ApiAppShop.Domain.Repositories.Base;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Constants;
using System;
using System.Threading.Tasks;

namespace ApiAppShop.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private IConfiguration _configuration;

        private readonly string ConexaoNoSql = RepositoryConstants.NO_SQL_CONECTION;

        private readonly string Db = RepositoryConstants.DB;

        private string _document;

        public Repository(IConfiguration config,
            string document)
        {
            _document = document;

            _configuration = config;
        }

        private IMongoDatabase GetDatabase() {
            MongoClient client = new MongoClient(
                _configuration.GetConnectionString(ConexaoNoSql));

            return client.GetDatabase(Db);
        }

        public async Task<T> GetItemAsync(string id) 
        {
            var filter = IdFilter(id);

            return await GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetItemByCustomStringFilterAsync(string field, string value)
        {            
            var filter = CustomStringFilter(field, value);

            return await GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            return await GetCollection().Find(_ => true).ToListAsync();
        }

        public async Task UpdateItemAsync(string itemId, string field, object value)
        {
            var filter = IdFilter(itemId);

            var update = Builders<T>.Update.Set(field, value);

            await GetCollection().UpdateOneAsync(filter, update);
        }

        public async Task ReplaceItemAsync(T item)
        {
            string itemId = item.Id;

            var filter = IdFilter(itemId);

            await GetCollection().ReplaceOneAsync(filter, item);
        }

        public async Task SetItemAsync(T item)
        {
            string id = item.Id;

            item.Id = id ?? Guid.NewGuid().ToString();

            await GetCollection().InsertOneAsync(item);
        }

        private FilterDefinition<T> IdFilter(string id) 
        {
            return Builders<T>.Filter.Eq(RepositoryConstants.ID, id);
        }

        private FilterDefinition<T> CustomStringFilter(string field, string value)
        {
            return Builders<T>.Filter.Eq(field, value);
        }

        private IMongoCollection<T> GetCollection() 
        {
            return GetDatabase().GetCollection<T>(_document);
        }
    }
}