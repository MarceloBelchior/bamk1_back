using System;
using System.Collections;
using Bank.App.IFace;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;

namespace Bank.App.Mongo.Repository.User
{
	public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase mongoDatabase;
        private readonly IConfigurationSection configurationSection;
        private const string collectionName = "User";

        public UserRepository(IConfigurationSection _configurationSection)
        {
            configurationSection = _configurationSection;
            var urlMongo = configurationSection.GetConnectionString("MongoURL") ?? string.Empty;
            mongoDatabase = GenericMongo.Create(urlMongo);
        }
        public async Task<Bank.App.Model.User> GetUserById(Bank.App.Model.User entity)
        {
            return await Task.Run(async () =>
            {
                var builder = Builders<UserEntity>.Filter;
                var query = Builders<UserEntity>.Filter.And(Builders<UserEntity>.Filter.Where(eq => eq.UserId == entity.UserId));
                var collection = mongoDatabase.GetCollection<UserEntity>(collectionName);
                var result = await collection.FindAsync(query);
                var _result = result.FirstOrDefault();
                if (_result != null)
                    return _result.Map();
                return null;
            });
        }

        public async Task<bool> SaveOrUpdate(Model.User entity)
        {
            return await Task.Run(async () =>
            {
                var builder = Builders<UserEntity>.Filter;

                var query = Builders<UserEntity>.Filter.And(Builders<UserEntity>.Filter.Where(eq => eq.UserId == entity.UserId));
                var collection = mongoDatabase.GetCollection<UserEntity>(collectionName);
                var result = await collection.ReplaceOneAsync(query, entity.Map(), new UpdateOptions() { IsUpsert = true });
            
                return result.MatchedCount > 0;
            });
        }

        public async Task<List<Bank.App.Model.User>> GetListUser()
        {
            return await Task.Run(async () =>
            {
                var builder = Builders<UserEntity>.Filter;
                var query = Builders<UserEntity>.Filter.And(Builders<UserEntity>.Filter.Where(eq => eq.Active == true));
                var collection = mongoDatabase.GetCollection<UserEntity>(collectionName);
                var result = await collection.FindAsync(query);
                var squery = result.ToList();
                if (squery.Count > 0)
                    return squery.Select(c => c.Map()).ToList();
                return null;
            });
        }

    }
}
