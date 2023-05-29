using System;
using Bank.App.Mongo.Repository.Account;
using MongoDB.Bson.Serialization.Attributes;

namespace Bank.App.Mongo.Repository.User
{

    [BsonIgnoreExtraElements]

    public class UserEntity
	{
        /// <summary>
        /// The ID of the user.
        /// </summary>
        ///
        [BsonId]
        public Guid UserId { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }

        public bool Active { get; set; }

   
    }
}

