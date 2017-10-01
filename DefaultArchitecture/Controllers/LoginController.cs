using DefaultArchitecture.Domain;
using DefaultArchitecture.Repository;
using DefaultArchitecture.Security;
using DefaultArchitecture.Security.JwtSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace DefaultArchitecture.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        [HttpPost]
        [AllowAnonymous]
        public string Login([FromBody]User user)
        {
            ISecurity<User> security = new JwtSecurity();
            return JsonConvert.SerializeObject(new
            {
                token = security.Login(user)
            });
        }

        
        [HttpGet]
        public string GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return JsonConvert.SerializeObject(new 
            {
                UserName = claimsIdentity.Name
            });
        }
    }
}
