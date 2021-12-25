using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ApiAppShop.Domain.Repositories.Base;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Constants;

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
            var filter = Builders<T>.Filter.Eq(RepositoryConstants.ID, id);
            return GetDatabase().GetCollection<T>(_document).Find(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetItems()
        {
            return GetDatabase().GetCollection<T>(_document).Find(_ => true).ToList();
        }

        public void SetItem(T item)
        {
            GetDatabase().GetCollection<T>(_document).InsertOne(item);
        }
    }
}