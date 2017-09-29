using DefaultArchitecture.Domain;
using DefaultArchitecture.Repository;
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

        private UserRepository userRepository;
        public LoginController()
        {
            this.userRepository = new UserRepository();
        }

        [HttpPost]
        [AllowAnonymous]
        public string GetAuthToken([FromBody]User user)
        {
            var existUser = userRepository.Login(user.Email, user.Password);

            if (existUser != null)
            {
                return JsonConvert.SerializeObject(new
                {
                    Token = GenerateToken(existUser)
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new 
                {
                    Msg = "Username or password is invalid"
                });
            }
        }

        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Token"),
                new[] {
                    new Claim("ID", user.ID.ToString())
                }
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Security.Issuer,
                Audience = Security.Audience,
                SigningCredentials = Security.SigningCredentials,
                Subject = identity,
                Expires = DateTime.Now.Add(Security.TokenExpirationTime),
                NotBefore = DateTime.Now
            });

            return handler.WriteToken(securityToken);
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
