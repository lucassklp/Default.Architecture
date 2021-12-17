using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Default.Architecture.Authentication.Jwt
{
    public static class JwtConfigure
    {
        public static void SetJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void SetJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = JwtTokenDefinitions.IssuerSigningKey,
                    ValidAudience = JwtTokenDefinitions.Audience,
                    ValidIssuer = JwtTokenDefinitions.Issuer,
                    ValidateIssuerSigningKey = JwtTokenDefinitions.ValidateIssuerSigningKey,
                    ValidateLifetime = JwtTokenDefinitions.ValidateLifetime,
                    ClockSkew = JwtTokenDefinitions.ClockSkew
                };
            });
        }
    }
}
