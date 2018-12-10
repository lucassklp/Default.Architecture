using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Domain;
using Domain.Dtos;
using Default.Architecture.Authentication;
using Business.Exceptions;

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
        public IActionResult Login([FromBody] CredentialDto user)
        {
            try
            {
                var token = authenticator.Login(user);
                return Ok(JsonConvert.SerializeObject(new
                {
                    token
                }));
            }
            catch (BusinessException)
            {
                return Unauthorized();
            }

        }
        
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return Ok(JsonConvert.SerializeObject(new 
            {
                UserName = claimsIdentity.Name,
                claimsIdentity.Claims
            }));
        }
    }
}
