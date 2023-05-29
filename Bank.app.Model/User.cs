using System;
namespace Bank.App.Model
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }



        /// <summary>
        /// The list of accounts owned by the user. just in case SQL 
        /// </summary>
       // public IList<Account> Accounts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set;  }

    }
}

