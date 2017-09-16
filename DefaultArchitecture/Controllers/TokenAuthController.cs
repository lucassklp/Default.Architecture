using DefaultArchitecture.Domain;
using DefaultArchitecture.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DefaultArchitecture.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {

        private UserRepository userRepository;
        public TokenAuthController()
        {
            this.userRepository = new UserRepository();
        }

        [HttpPost]
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
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(new RSACryptoServiceProvider(2048).ExportParameters(true)), SecurityAlgorithms.RsaSha256Signature),
                Subject = identity,
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(30)),
                NotBefore = DateTime.Now
            });

            return handler.WriteToken(securityToken);
        }

        [HttpGet]
        [AllowAnonymous]
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
