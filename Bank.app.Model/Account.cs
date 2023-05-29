using System;


namespace Bank.App.Model
{
    /// <summary>
    /// Represents an account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// The ID of the account.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// The balance of the account.
        /// </summary>
        public decimal Balance { get; set; }


        public bool Active { get; set; }


    }
}