using System.Reactive.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Domain.Dtos;
using Default.Architecture.Authentication;

namespace Default.Architecture.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        IAuthenticatorAsync<ICredential> authenticator;
        public LoginController(IAuthenticatorAsync<ICredential> authenticator)
        {
            this.authenticator = authenticator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CredentialDto user)
        {
            return await authenticator.LoginAsync(user).Select(token => Json(new { token }));
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
