using Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reactive.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Domain.Entities;
using Business.Interfaces;
using System.Reactive.Disposables;

namespace Default.Architecture.Authentication.Jwt
{
    public class JwtAuthenticator : IAuthenticator<ICredential>
    {
        private ILoginServices loginService;
        public JwtAuthenticator(ILoginServices repository)
        {
            loginService = repository;
        }

        public IObservable<string> Login(ICredential credential)
        {
            return loginService.Login(credential).SelectMany(user =>
            {
                if (user != null)
                {
                    return this.GenerateToken(user);
                }
                else return null;
            });
        }

        private IObservable<string> GenerateToken(User user)
        {
            return Observable.Create<string>(observer =>
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

                observer.OnNext(handler.WriteToken(securityToken));
                return Disposable.Empty;
            });
        }
    }
}
