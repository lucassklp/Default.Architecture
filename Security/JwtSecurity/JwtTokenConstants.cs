using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Security.JwtSecurity
{
    public class JwtTokenConstants
    {
        public static SecurityKey IssuerSigningKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
            }
        }

        public static SigningCredentials SigningCredentials
        {
            get
            {
                return new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
            }
        }
        
        public static TimeSpan TokenExpirationTime
        {
            get
            {
                return TimeSpan.FromHours(2);
            }
        }

        public static string Issuer
        {
            get
            {
                return "Issuer";
            }
        }

        public static string Audience
        {
            get
            {
                return "Audience";
            }
        }

        private static string _key;

        public static String Key
        {
            get => _key;
            set => _key = value.ToMD5();
        }
    }
}
