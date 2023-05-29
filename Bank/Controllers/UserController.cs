using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.App.IFace;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly  IUserBusiness  userBusiness;
        private readonly IAccountBusiness accountBusiness;
        public UserController(IUserBusiness _userBusiness, IAccountBusiness _accountBusiness)
        {
            userBusiness = _userBusiness;
            accountBusiness = _accountBusiness;
        }


        [HttpGet, Route("Users")]
        [SwaggerResponse(200, "List of users", typeof(IList<Model.User>))]
     
        public async Task<IActionResult> GetUser()
        {
           var user =  await userBusiness.GetUserActive();
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPatch, Route("Users")]

        public async Task<IActionResult> Updateuser([FromBody] Model.User user)
        {
            var result = await userBusiness.UpdateUser(user);
            return Ok(result);
        }
        [HttpDelete, Route("Users")]

        public async Task<IActionResult> DeleteUser([FromBody] Model.User user)
        {
            user.Active = false;
            return Ok(await userBusiness.UpdateUser(user));
        }

        [HttpPost, Route("Users")]

        public async Task<IActionResult> CreateUser([FromBody] Model.User user)
        {
            //user.UserId = System.Guid.NewGuid();
            user.Active = true;
            user.UserId = System.Guid.NewGuid();
            await userBusiness.UpdateUser(user);

            await accountBusiness.CreateAccount(new Model.Account()
            {
                AccountId = System.Guid.NewGuid(),
                Balance = 0,
                Active = true,
                UserId = user.UserId
            });
            return Ok(user); 
        }


    }
}

