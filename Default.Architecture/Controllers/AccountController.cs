using Business.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System;

namespace Default.Architecture.Controllers
{
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IUserServices userServices;

        public AccountController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            IObservable<IActionResult> response = userServices.RegisterAsync(user).Select(registredUser =>
            {
                return Ok(registredUser);
            });
            response.Subscribe();
            return await response;

        }
    }
}
