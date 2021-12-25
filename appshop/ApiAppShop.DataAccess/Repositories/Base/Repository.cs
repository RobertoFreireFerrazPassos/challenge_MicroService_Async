using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ApiAppShop.Domain.Repositories.Base;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Constants;
using System;
using MongoDB.Bson;

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

        public T GetItem(string id) 
        {
            var filter = IdFilter(id);
            return GetCollection().Find(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetItems()
        {
            return GetCollection().Find(_ => true).ToList();
        }

        public void UpdateItem(string itemId, string field, object value)
        {
            var filter = IdFilter(itemId);
            var update = Builders<T>.Update.Set(field, value);
            GetCollection().UpdateOne(filter, update);
        }

        public void ReplaceItem(T item)
        {
            string itemId = item.Id;
            var filter = IdFilter(itemId);
            GetCollection().ReplaceOne(filter, item);
        }

        public void SetItem(T item)
        {
            string id = item.Id;
            var filter = IdFilter(id);
            item.Id = id ?? Guid.NewGuid().ToString();
            GetCollection().InsertOne(item);
        }
        private FilterDefinition<T> IdFilter(string id) 
        {
            return Builders<T>.Filter.Eq(RepositoryConstants.ID, id);
        }

        private IMongoCollection<T> GetCollection() 
        {
            return GetDatabase().GetCollection<T>(_document);
        }
    }
}