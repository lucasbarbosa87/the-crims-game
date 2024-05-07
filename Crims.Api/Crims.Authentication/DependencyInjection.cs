using Crims.Data.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Crims.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCrimsAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Auth");
            services.AddScoped(a => new AuthConfiguration(
                Secret: section.GetSection("Token").GetValue<string>("Secret") ?? "",
                RefreshTokenExpires: section.GetSection("Token").GetValue<int>("RefreshTokenExpires"),
                ExpiresIn: section.GetSection("Token").GetValue<int>("ExpiresIn"),
                ValidAudience: section.GetSection("Configuration").GetValue<string>("ValidAudience") ?? ""
                )
            );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = section.GetSection("Configuration").GetValue<string>("ValidAudience"),
                    ValidIssuer = section.GetSection("Configuration").GetValue<string>("ValidIssuer"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section.GetSection("Token").GetValue<string>("Secret") ?? ""))
                };
            });
            return services;
        }
    }
}
