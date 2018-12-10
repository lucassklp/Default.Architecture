using Business.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Reactive.Linq;
namespace Default.Architecture.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IUserServices userServices;
        private ILogger logger;
        private IConfiguration configuration;

        public AccountController(IUserServices userServices, 
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            this.userServices = userServices;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var x = await userServices.RegisterAsync(user);
            return Ok(x);
        }
    }
}
