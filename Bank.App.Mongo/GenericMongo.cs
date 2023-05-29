using System;
using MongoDB.Driver;

namespace Bank.App.Mongo
{
    internal static class GenericMongo
    {
        internal static IMongoDatabase Create(string mongoUrl)
        {
            if (string.IsNullOrEmpty(mongoUrl))
            {
                var mongoUrlEnv = System.Environment.GetEnvironmentVariable("mongoUrl");
                if (string.IsNullOrEmpty(mongoUrlEnv))
                    throw new Exception("Connection string not found!");
                else
                    mongoUrl = mongoUrlEnv;
            }

            var url = MongoUrl.Create(mongoUrl);
            var client = new MongoClient(url);


            return client.GetDatabase(url.DatabaseName);
        }
    }
}

