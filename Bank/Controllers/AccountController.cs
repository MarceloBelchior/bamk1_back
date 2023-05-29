using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Bank.App.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IFace.IAccountBusiness accountBusiness;
        public AccountController(IFace.IAccountBusiness _accountBusiness) => accountBusiness = _accountBusiness;


        [HttpGet, Route("{userid}")]
        [SwaggerResponse(200, "List of Account for the user", typeof(IList<Model.Account>))]

        public async Task<IActionResult> GetAccount([FromRoute] Guid userid)
        {
           // Guid UserId = Guid.Empty;
           // Guid.TryParse(userid, out UserId);
            if (userid == Guid.Empty)
                return BadRequest("User key Invalid");
            var query = await accountBusiness.GetAccountByUserID(new Model.Account() { UserId = userid });
            if (query.Count == 0)
                return NotFound();
            return Ok(query);
        }

        [HttpPost, Route("{userid}/DeleteAccount")]
        public async Task<IActionResult> RemoveAccount([FromRoute] Guid userid, [FromBody] Account account)
        {
        
            if (userid == Guid.Empty)
                return BadRequest("User key Invalid");
            if (account.AccountId == Guid.Empty)
                return BadRequest("Account key Invalid");
           
            var query = accountBusiness.DeleteAccount(account);
            return Ok();
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create( [FromBody] Account account)
        {
            account.AccountId = System.Guid.NewGuid();
            account.Active = true;
            await accountBusiness.CreateAccount(account);
            return Ok(account);
        }



        [HttpPost, Route("{accountid}/{amount}/withdraw")]

        public async Task<IActionResult> withdraw([FromRoute] Guid accountid, [FromRoute] Decimal amount)
        {
   
            if (accountid == Guid.Empty)
                return BadRequest("Account key Invalid");




            var transaction = await accountBusiness.GetAccountByID(new Account() {  AccountId = accountid });
            if (transaction == null)
                return BadRequest("Account Number not found");

            if (amount > (transaction.Balance * 0.9m))
            {
                return BadRequest("Withdrawal amount exceeds the limit.");
            }
            if (transaction.Balance - amount < 100) 
            {
                return BadRequest("Insufficient balance after withdrawal.");
            }
            transaction.Balance -= amount;


            await accountBusiness.Update(transaction);

            return Ok(transaction);
        }

        [HttpPost, Route("{accountid}/{amount}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute] Guid accountid, [FromRoute] Decimal amount)
        {

   
            if (accountid == Guid.Empty)
                return BadRequest("Account key Invalid");

            var transaction = await accountBusiness.GetAccountByID(new Account() { AccountId = accountid });
            if (transaction == null)
                return BadRequest("Account Number not found");

            if (amount > 10000)
            {
                return BadRequest("Deposit amount exceeds the limit.");
            }

            transaction.Balance += amount;
            await accountBusiness.Update(transaction);

          
            return Ok(transaction);
        }


    }
}

