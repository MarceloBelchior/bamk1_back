using System;
using Bank.App.IFace;
using Bank.App.Mongo.Repository.User;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Bank.App.Mongo.Repository.Account
{
	public class AccountRepository : IAccountRepository
    {
        private readonly IMongoDatabase mongoDatabase;
        private readonly IConfigurationSection configurationSection;
        private const string collectionName = "Account";

        public AccountRepository(IConfigurationSection _configurationSection)
        {
            configurationSection = _configurationSection;
            var urlMongo = configurationSection.GetConnectionString("MongoURL") ?? string.Empty;
            mongoDatabase = GenericMongo.Create(urlMongo);
        }
        public async Task<IList<Bank.App.Model.Account>> GetUserById(Bank.App.Model.Account entity)
        {
            return await Task.Run(async () =>
            {
                var builder = Builders<AccountEntity>.Filter;
                var query = Builders<AccountEntity>.Filter.And(Builders<AccountEntity>.Filter.Where(eq => eq.UserId == entity.UserId && eq.Active == true));
                var collection = mongoDatabase.GetCollection<AccountEntity>(collectionName);
                var _result = await collection.FindAsync(query);
                var tresult = _result.ToList();
                if (tresult != null)
                    return tresult.Select(c => c.Map()).ToList();
                return null;
            });
        }
        public async Task<Bank.App.Model.Account> GeById(Bank.App.Model.Account entity)
        {
            return await Task.Run(async () =>
            {
                var builder = Builders<AccountEntity>.Filter;
                var query = Builders<AccountEntity>.Filter.And(Builders<AccountEntity>.Filter.Where(eq => eq.AccountId == entity.AccountId));
                var collection = mongoDatabase.GetCollection<AccountEntity>(collectionName);
                var _result = await collection.FindAsync(query);
                var tresult = _result.FirstOrDefault();
                if (tresult.AccountId != Guid.Empty)
                    return tresult.Map();
                return null;
            });
        }



        public async Task<bool> SaveOrUpdate(Model.Account entity)
        {
            return await Task.Run(async () =>
            {
                var builder = Builders<AccountEntity>.Filter;

                var query = Builders<AccountEntity>.Filter.And(Builders<AccountEntity>.Filter.Where(eq => eq.AccountId == entity.AccountId));
                var collection = mongoDatabase.GetCollection<AccountEntity>(collectionName);
                var result = await collection.ReplaceOneAsync(query, entity.Map(), new UpdateOptions() { IsUpsert = true });

                return result.MatchedCount > 0;
            });
        }

    }
}

