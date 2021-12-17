using System.Linq;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ApiAppShop.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private IConfiguration _configuration;
        private readonly string ConexaoNoSql = "ConexaoNoSql";
        private readonly string Db = "DB";

        public Repository(IConfiguration config)
        {
            _configuration = config;
        }

        private IMongoDatabase GetDatabase() {
            MongoClient client = new MongoClient(
                _configuration.GetConnectionString(ConexaoNoSql));
            return client.GetDatabase(Db);
        }

        public T GetItem<T>(string id, string table) 
        {
            IMongoDatabase db = GetDatabase();

            var filter = Builders<T>.Filter.Eq("Id", id);

            try
            {
                return db.GetCollection<T>(table).Find(filter).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
        }

        public IEnumerable<T> GetItems<T>(string table)
        {
            IMongoDatabase db = GetDatabase();

            try
            {
                return db.GetCollection<T>(table).Find(_ => true).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
        }

        public void SetItem<T>(T item,string table)
        {
            IMongoDatabase db = GetDatabase();

            try
            {
                db.GetCollection<T>(table).InsertOne(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }
    }
}