using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using ApiUser.Models;
using System;
using System.Collections.Generic;

namespace ApiAppShop.Repository
{
    public class Context
    {
        private IConfiguration _configuration;

        public Context(IConfiguration config)
        {
            _configuration = config;
        }

        public IEnumerable<T> GetItems<T>(string id)
        {
            MongoClient client = new MongoClient(
                _configuration.GetConnectionString("ConexaoNoSql"));
            IMongoDatabase db = client.GetDatabase("DBUser");

            var filter = Builders<T>.Filter.Eq("Id", id);

            try
            {
                return db.GetCollection<T>("Users").Find(_ => true).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        public void SetItem<T>(T item)
        {
            MongoClient client = new MongoClient(
                _configuration.GetConnectionString("ConexaoNoSql"));
            IMongoDatabase db = client.GetDatabase("DBUser");

            try
            {
                db.GetCollection<T>("Users").InsertOne(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }            
        }
    }
}