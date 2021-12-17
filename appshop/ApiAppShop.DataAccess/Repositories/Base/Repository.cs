using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ApiAppShop.Domain.Repositories.Base;
using ApiAppShop.Domain.Entities;

namespace ApiAppShop.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private IConfiguration _configuration;
        private readonly string ConexaoNoSql = "ConexaoNoSql";
        private readonly string Db = "DB";
        private string _table;

        public Repository(IConfiguration config,
            string table)
        {
            _table = table;
            _configuration = config;
        }

        private IMongoDatabase GetDatabase() {
            MongoClient client = new MongoClient(
                _configuration.GetConnectionString(ConexaoNoSql));
            return client.GetDatabase(Db);
        }

        public T GetItem(string id) 
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return GetDatabase().GetCollection<T>(_table).Find(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetItems()
        {
            return GetDatabase().GetCollection<T>(_table).Find(_ => true).ToList();
        }

        public void SetItem(T item)
        {
            GetDatabase().GetCollection<T>(_table).InsertOne(item);
        }
    }
}