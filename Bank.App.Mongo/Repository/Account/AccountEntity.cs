using System;
using Bank.App.Mongo.Repository.User;
using MongoDB.Bson.Serialization.Attributes;

namespace Bank.App.Mongo.Repository.Account
{
    [BsonIgnoreExtraElements]
    public class AccountEntity
	{
        public Guid UserId { get; set; }
        /// <summary>
        /// The ID of the account.
        /// </summary>
        ///
        [BsonId]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The balance of the account.
        /// </summary>
        public decimal Balance { get; set; }

        public bool Active { get; set; }


    }
}

