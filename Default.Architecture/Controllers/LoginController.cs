using System.Reactive.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Domain.Dtos;
using Default.Architecture.Authentication;
using System;
using System.Reactive.Threading.Tasks;

namespace Default.Architecture.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        IAuthenticator<ICredential> authenticator;
        public LoginController(IAuthenticator<ICredential> authenticator)
        {
            this.authenticator = authenticator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CredentialDto user)
        {
            IObservable<JsonResult> response = authenticator.Login(user).Select(token =>
            {
                return Json(new { token });
            });
            response.Subscribe();

            return await response;
        }
        
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return Json(new 
            {
                UserName = claimsIdentity.Name,
                claimsIdentity.Claims
            });
        }
    }
}
