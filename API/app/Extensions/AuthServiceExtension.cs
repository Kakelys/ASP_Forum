using System.Text;
using app.Interfaces;
using app.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace app.Extensions
{
    public static class AuthServiceExtension
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt key is empty")))
                };
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy("RequireAdminOrModeratorRole", policy => policy.RequireRole("admin", "moderator"));
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
                options.AddPolicy("RequireModeratorRole", policy => policy.RequireRole("moderator"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("user"));
            });

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
        
    }
}