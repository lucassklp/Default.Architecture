using Default.Architecture.Business;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Default.Architecture.Controllers
{
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private UserServices userServices;

        public AccountController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            return Ok(await userServices.RegisterAsync(user));
        }
    }
}
