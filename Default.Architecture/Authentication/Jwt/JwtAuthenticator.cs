using Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Domain.Entities;
using Business.Interfaces;

namespace Default.Architecture.Authentication.Jwt
{
    public class JwtAuthenticator : IAuthenticator<ICredential>
    {
        private ILoginServices loginService;
        public JwtAuthenticator(ILoginServices repository)
        {
            loginService = repository;
        }

        public string Login(ICredential credential)
        {
            var existUser = loginService.Login(credential);

            if (existUser != null)
            {
                return this.GenerateToken(existUser);
            }
            else return null;
        }

        public string Logout(ICredential credential)
        {
            this.loginService.Logout(credential);
            return null;
        }

        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Description));
            }

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(ClaimTypes.NameIdentifier, user.ID.ToString()), claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
                Subject = identity,
                Expires = DateTime.Now.Add(JwtTokenDefinitions.TokenExpirationTime),
                NotBefore = DateTime.Now
            });
            
            return handler.WriteToken(securityToken);
        }
    }
}
