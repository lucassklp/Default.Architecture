using Security;
using DefaultArchitecture.Security.JwtSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Persistence;
using Domain.Entities;

namespace DefaultArchitecture.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        DaoContext context;
        public LoginController(DaoContext daoContext)
        {
            this.context = daoContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]User user)
        {
            ISecurity<User> security = new JwtSecurity(context);
            return Ok(JsonConvert.SerializeObject(new
            {
                token = security.Login(user)
            }));
        }

        
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return Ok(JsonConvert.SerializeObject(new 
            {
                UserName = claimsIdentity.Name,
                Claims = claimsIdentity.Claims
            }));
        }
    }
}
