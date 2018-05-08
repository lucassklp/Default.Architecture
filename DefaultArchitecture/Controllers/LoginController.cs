using Security;
using DefaultArchitecture.Security.JwtSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Persistence;
using Domain.Entities;
using Repository;

namespace DefaultArchitecture.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        DaoContext context;
        UserRepository userRepository;
        public LoginController(DaoContext daoContext, UserRepository userRepository)
        {
            this.context = daoContext;
            this.userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]User user)
        {
            ISecurity<User> security = new JwtSecurity(userRepository);
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
