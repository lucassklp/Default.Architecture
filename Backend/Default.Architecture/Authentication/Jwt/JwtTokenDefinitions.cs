using Core.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Default.Architecture.Authentication.Jwt
{
    public class JwtTokenDefinitions
    {
        public static void LoadFromConfiguration(IConfiguration configuration)
        {
            var config = configuration.GetSection("JwtConfiguration");
            Key = config.GetValue<string>("Key");
            Audience = config.GetValue<string>("Audience");
            Issuer = config.GetValue<string>("Issuer");
            TokenExpirationTime = TimeSpan.FromMinutes(config.GetValue<int>("TokenExpirationTime"));
            ValidateIssuerSigningKey = config.GetValue<bool>("ValidateIssuerSigningKey");
            ValidateLifetime = config.GetValue<bool>("ValidateLifetime");
            ClockSkew = TimeSpan.FromMinutes(config.GetValue<int>("ClockSkew"));
        }

        private static string _key = "";
        public static string Key
        {
            get => _key;
            set => _key = value.ToMD5();
        }

        public static SecurityKey IssuerSigningKey
        {
            get => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public static SigningCredentials SigningCredentials
        {
            get => new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        public static TimeSpan TokenExpirationTime { get; set; } = TimeSpan.FromHours(60);

        public static TimeSpan ClockSkew { get; set; } = TimeSpan.FromHours(0);

        public static string Issuer { get; set; } = "";

        public static string Audience { get; set; } = "";

        public static bool ValidateIssuerSigningKey { get; set; } = true;

        public static bool ValidateLifetime { get; set; } = true;

    }
}
