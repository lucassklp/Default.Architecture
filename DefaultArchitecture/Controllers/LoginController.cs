using Domain;
using Repository;
using Security;
using Security.JwtSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Persistence;

namespace DefaultArchitecture.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        DaoContext context;
        public LoginController(DaoContext daoContext)
        {
            this.context = daoContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public string Login([FromBody]User user)
        {
            ISecurity<User> security = new JwtSecurity(context);
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
